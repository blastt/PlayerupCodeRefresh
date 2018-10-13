using AutoMapper;
using Hangfire;
using MarketplaceMVC.Model.Models;
using MarketplaceMVC.Service;
using MarketplaceMVC.Web.Hangfire;
using MarketplaceMVC.Web.Models.AccountInfo;
using MarketplaceMVC.Web.Models.Checkout;
using MarketplaceMVC.Web.Models.Withdraw;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MarketplaceMVC.Web.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IOfferService offerService;
        private readonly IAccountInfoService accountInfoService;
        private readonly IUserProfileService userProfileService;
        private readonly IOrderService orderService;
        private readonly IWithdrawService withdrawService;
        private readonly IOrderStatusService orderStatusService;
        private readonly ITransactionService transactionService;

        public CheckoutController(IOfferService offerService, IUserProfileService userProfileService, IOrderService orderService, IOrderStatusService orderStatusService, ITransactionService transactionService)
        {
            this.transactionService = transactionService;
            this.orderService = orderService;
            this.offerService = offerService;
            this.userProfileService = userProfileService;
            this.orderStatusService = orderStatusService;
        }

        [HttpGet]
        public async Task<ActionResult> Buy(int? id)
        {
            if (id != null)
            {
                var offer = await offerService.GetOfferAsync(id.Value, o => o.Game, o => o.UserProfile);
                if (offer != null && offer.Order == null && offer.State == OfferState.active && offer.UserProfileId != User.Identity.GetUserId<int>())
                {
                    var userId = User.Identity.GetUserId<int>();
                    var user = await userProfileService.GetUserProfileByIdAsync(userId);
                    CheckoutViewModel model = new CheckoutViewModel()
                    {
                        OfferHeader = offer.Header,
                        OfferId = offer.Id,
                        Game = offer.Game.Name,
                        SellerPaysMiddleman = offer.SellerPaysMiddleman,
                        MiddlemanPrice = offer.MiddlemanPrice.Value,
                        OrderSum = offer.Price,
                        Quantity = 1,
                        SellerId = offer.UserProfile.Id,
                        BuyerId = userId
                    };
                    if (offer.SellerPaysMiddleman)
                    {
                        model.OrderSum = offer.Price;
                    }
                    else
                    {
                        model.OrderSum = offer.Price + offer.MiddlemanPrice.Value;
                    }
                    model.UserCanPayWithBalance = user.Balance >= model.OrderSum;
                    return View(model);
                }

            }
            return HttpNotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Buy(CheckoutViewModel model)
        {
            Offer offer = await offerService.GetOfferAsync(model.OfferId, o => o.UserProfile, o => o.UserProfile.User, o => o.Order, o => o.Order.StatusLogs, o => o.Order.CurrentStatus);
            if (offer != null && offer.Order == null && offer.State == OfferState.active && offer.UserProfileId != User.Identity.GetUserId<int>())
            {
                var currentUserId = User.Identity.GetUserId<int>();
                var user = userProfileService.GetUserProfile(u => u.Id == currentUserId, i => i.User);
                var mainCup = userProfileService.GetUserProfileByName("palyerup");
                if (user != null && mainCup != null && user.Balance >= model.OrderSum)
                {
                    //var amount = Decimal.Parse(Request.Form["ik_am"]);

                    offer.Order = new Order
                    {
                        BuyerId = currentUserId,
                        SellerId = offer.UserProfileId,
                        CreatedDate = DateTime.Now
                    };
                    offer.State = OfferState.closed;


                    if (mainCup != null)
                    {
                        var seller = offer.UserProfile;
                        var buyer = user;

                        if (offer.SellerPaysMiddleman)
                        {
                            offer.Order.Sum = offer.Price;
                            offer.Order.AmmountSellerGet = offer.Price - offer.MiddlemanPrice.Value;
                            if (buyer.Balance >= offer.Order.Sum)
                            {
                                transactionService.CreateTransaction(new Transaction
                                {
                                    Amount = offer.Order.Sum,
                                    Order = offer.Order,
                                    Receiver = mainCup,
                                    Sender = buyer,
                                    CreatedDate = DateTime.Now
                                });
                                buyer.Balance -= offer.Order.Sum;
                                mainCup.Balance += offer.Order.Sum;
                            }
                            else
                            {
                                return View("NotEnoughMoney");
                            }
                        }
                        else
                        {
                            offer.Order.Sum = offer.Price + offer.MiddlemanPrice.Value;
                            offer.Order.AmmountSellerGet = offer.Price;
                            if (buyer.Balance >= offer.Order.Sum)
                            {
                                transactionService.CreateTransaction(new Transaction
                                {
                                    Amount = offer.Order.Sum,
                                    Order = offer.Order,
                                    Receiver = mainCup,
                                    Sender = buyer,
                                    CreatedDate = DateTime.Now
                                });
                                buyer.Balance -= offer.Order.Sum;
                                mainCup.Balance += offer.Order.Sum;
                            }
                            else
                            {
                                return View("NotEnoughMoney");
                            }
                        }
                        offer.Order.StatusLogs.AddLast(new StatusLog()
                        {
                            OldStatus = orderStatusService.GetOrderStatusByValue(OrderStatuses.OrderCreating),
                            NewStatus = orderStatusService.GetOrderStatusByValue(OrderStatuses.BuyerPaying),
                            TimeStamp = DateTime.Now
                        });

                        offer.Order.StatusLogs.AddLast(new StatusLog()
                        {
                            OldStatus = orderStatusService.GetOrderStatusByValue(OrderStatuses.BuyerPaying),
                            NewStatus = orderStatusService.GetOrderStatusByValue(OrderStatuses.MiddlemanFinding),
                            TimeStamp = DateTime.Now
                        });
                        offer.Order.CurrentStatus = orderStatusService.GetOrderStatusByValue(OrderStatuses.MiddlemanFinding);

                        offer.Order.BuyerChecked = false;
                        offer.Order.SellerChecked = false;

                        if (offer.Order.JobId != null)
                        {
                            BackgroundJob.Delete(offer.Order.JobId);
                            offer.Order.JobId = null;
                        }

                        if (offer.JobId != null)
                        {
                            BackgroundJob.Delete(offer.JobId);
                            offer.JobId = null;
                        }

                        orderService.SaveOrder();

                        MarketplaceMVCHangfire.SetSendEmailChangeStatus(offer.Order.Id, seller.User.Email, offer.Order.CurrentStatus.DuringName, Url.Action("SellDetails", "Order", new { id = offer.Order.Id }, protocol: Request.Url.Scheme));

                        MarketplaceMVCHangfire.SetSendEmailChangeStatus(offer.Order.Id, buyer.User.Email, offer.Order.CurrentStatus.DuringName, Url.Action("BuyDetails", "Order", new { id = offer.Order.Id }, protocol: Request.Url.Scheme));
                        offer.Order.JobId = MarketplaceMVCHangfire.SetOrderCloseJob(offer.Order.Id, TimeSpan.FromDays(1));
                        //_orderService.UpdateOrder(order);
                        orderService.SaveOrder();
                        TempData["orderBuyStatus"] = "Оплата прошла успешно";

                        return RedirectToAction("BuyDetails", "Order", new { id = offer.Order.Id });
                    }

                }
            }
            return HttpNotFound();
        }

        public ActionResult BalanceOperations()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CashIn()
        {
            string userId = User.Identity.GetUserId();
            return View((object)userId);
        }




        [HttpGet]
        public ActionResult CashOut()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Withdraw(CreateWithdrawViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId<int>();
                var user = await userProfileService.GetUserProfileByIdAsync(userId);
                var withdraw = Mapper.Map<CreateWithdrawViewModel, Withdraw>(model);
                withdraw.User = user;
                if (user.Balance >= withdraw.Amount && withdraw.Amount > 50)
                {
                    user.Balance -= withdraw.Amount;

                    withdrawService.CreateWithdraw(withdraw);
                    withdrawService.SaveWithdraw();
                    return View("SuccessWithdraw");
                }
                return View("ErrorWithdraw");
            }
            return HttpNotFound();


        }

        [HttpGet]
        public async Task<ActionResult> ProvideData(int? Id, int? moderatorId, int? buyerId, int? sellerId)
        {
            if (Id != null && moderatorId != null && buyerId != null && sellerId != null)
            {
                Order order = await orderService.GetOrderAsync(Id.Value, i => i.Middleman, i => i.Seller, i => i.Buyer, i => i.CurrentStatus, i => i.Offer);
                if (sellerId == User.Identity.GetUserId<int>())
                {
                    if (order.MiddlemanId == moderatorId && order.SellerId == sellerId &&
                    order.BuyerId == buyerId && order.CurrentStatus.Value == OrderStatuses.SellerProviding)
                    {
                        AccountInfoViewModel model = new AccountInfoViewModel
                        {
                            ModeratorId = moderatorId.Value,
                            AccountLogin = order.Offer.AccountLogin,
                            BuyerId = buyerId.Value,
                            SellerId = sellerId.Value

                        };
                        return View(model);
                    }
                }

            }
            return HttpNotFound();
        }

        [HttpPost]
        public async Task<ActionResult> ProvideData(AccountInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var accountInfo = Mapper.Map<AccountInfoViewModel, AccountInfo>(model);

                var moderator = await userProfileService.GetUserProfileAsync(u => u.Id == model.ModeratorId, i => i.User);

                bool moderIsInrole = true;
                if (moderator != null)
                {
                    foreach (var role in moderator.User.Roles)
                    {
                        if (role.RoleId == 2 && role.UserId == moderator.Id)
                        {
                            moderIsInrole = true;
                        }
                    }
                }
                if (moderIsInrole)
                {
                    var order = orderService.GetOrder(model.AccountLogin, model.ModeratorId, model.SellerId,
                        model.BuyerId, i => i.CurrentStatus, i => i.StatusLogs, i => i.Seller.User, i => i.Buyer.User);
                    if (order != null)
                    {
                        if (order.CurrentStatus != null)
                        {
                            if (order.CurrentStatus.Value == OrderStatuses.SellerProviding)
                            {
                                order.StatusLogs.AddLast(new StatusLog()
                                {
                                    OldStatus = order.CurrentStatus,
                                    NewStatus = orderStatusService.GetOrderStatusByValue(OrderStatuses.MiddlemanChecking),
                                    TimeStamp = DateTime.Now
                                });
                                order.CurrentStatus = orderStatusService.GetOrderStatusByValue(OrderStatuses.MiddlemanChecking);


                                order.BuyerChecked = false;
                                order.SellerChecked = false;
                                accountInfo.Order = order;
                                accountInfoService.CreateAccountInfo(accountInfo);
                                if (order.JobId != null)
                                {
                                    BackgroundJob.Delete(order.JobId);
                                    order.JobId = null;
                                }
                                //order.JobId = MarketHangfire.SetOrderCloseJob(order.Id, TimeSpan.FromMinutes(5));
                                await orderService.SaveOrderAsync();


                                MarketplaceMVCHangfire.SetSendEmailChangeStatus(order.Id, order.Seller.User.Email, order.CurrentStatus.DuringName, Url.Action("SellDetails", "Order", new { id = order.Id }, protocol: Request.Url.Scheme));

                                MarketplaceMVCHangfire.SetSendEmailChangeStatus(order.Id, order.Buyer.User.Email, order.CurrentStatus.DuringName, Url.Action("BuyDetails", "Order", new { id = order.Id }, protocol: Request.Url.Scheme));
                                TempData["orderSellStatus"] = "Ваши данные были отправлены на проверку гаранту";
                                return RedirectToAction("SellDetails", "Order", new { id = order.Id });
                            }
                        }
                    }
                }

            }
            return HttpNotFound();
        }

        public async Task<ActionResult> ConfirmOrder(int? id)
        {
            if (id != null)
            {
                bool result = orderService.ConfirmOrder(id.Value, User.Identity.GetUserId<int>());
                var order = orderService.GetOrder(id.Value, i => i.CurrentStatus, i => i.Seller.User, i => i.Buyer.User);
                if (result && order != null)
                {
                    if (order.JobId != null)
                    {
                        BackgroundJob.Delete(order.JobId);
                        order.JobId = null;
                    }

                    await orderService.SaveOrderAsync();

                    order.JobId = MarketplaceMVCHangfire.SetLeaveFeedbackJob(order.SellerId, order.BuyerId.Value, order.Id, TimeSpan.FromDays(15));


                    MarketplaceMVCHangfire.SetSendEmailChangeStatus(order.Id, order.Seller.User.Email, order.CurrentStatus.DuringName, Url.Action("SellDetails", "Order", new { id = order.Id }, protocol: Request.Url.Scheme));

                    MarketplaceMVCHangfire.SetSendEmailChangeStatus(order.Id, order.Buyer.User.Email, order.CurrentStatus.DuringName, Url.Action("BuyDetails", "Order", new { id = order.Id }, protocol: Request.Url.Scheme));

                    await orderService.SaveOrderAsync();


                    TempData["orderBuyStatus"] = "Спасибо за подтверждение сделки! Сделка успешно закрыта.";
                    return RedirectToAction("BuyDetails", "Order", new { id });
                }
            }
            return HttpNotFound();
        }
    }
}