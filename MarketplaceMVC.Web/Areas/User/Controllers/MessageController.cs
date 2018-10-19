using AutoMapper;
using MarketplaceMVC.Model.Models;
using MarketplaceMVC.Service;
using MarketplaceMVC.Web.Areas.User.Models.Message;
using MarketplaceMVC.Web.SignalrHubs;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace MarketplaceMVC.Web.Areas.User.Controllers
{
    
    public class MessageController : Controller
    {
        private readonly IMessageHub messageHub;
        private readonly IUserProfileService userProfileService;
        private readonly IMessageService messageService;
        private readonly IOfferService offerService;
        private readonly IDialogService dialogService;

        public MessageController(IMessageService messageService, IOfferService offerService, IUserProfileService userProfileService, IDialogService dialogService, IMessageHub messageHub)
        {
            this.messageHub = messageHub;
            this.messageService = messageService;
            this.offerService = offerService;
            this.userProfileService = userProfileService;
            this.dialogService = dialogService;
        }
        [HttpPost]
        public async Task<ActionResult> Create(MessageViewModel model)
        {


            if (ModelState.IsValid)
            {
                if (model.MessageBody.Trim() == "")
                {
                    return Json(new { success = false, responseText = "Вы не ввели сообщение" }, JsonRequestBehavior.AllowGet);
                }


                var toUser = userProfileService.GetUserProfile(u => u.Id == model.ReceiverId, u => u.DialogsAsСompanion, u => u.DialogsAsCreator,
                    u => u.DialogsAsCreator.Select(i => i.Messages), u => u.DialogsAsСompanion.Select(i => i.Messages));
                var currentUserId = User.Identity.GetUserId<int>();
                var fromUser = await userProfileService.GetUserProfileAsync(u => u.Id == currentUserId, u => u.DialogsAsСompanion, u => u.DialogsAsCreator,
                    u => u.DialogsAsCreator.Select(i => i.Messages), u => u.DialogsAsСompanion.Select(i => i.Messages));
                if (toUser != null && fromUser != null && toUser.Id != fromUser.Id)
                {

                    Message message = Mapper.Map<MessageViewModel, Message>(model);
                    message.FromViewed = true;
                    message.SenderId = fromUser.Id;
                    message.CreatedDate = DateTime.Now;
                    var privateDialog = dialogService.GetPrivateDialog(toUser, fromUser);

                    if (privateDialog == null)
                    {
                        privateDialog = new Dialog()
                        {
                            CreatorId = fromUser.Id,
                            CompanionId = toUser.Id
                        };

                        dialogService.CreateDialog(privateDialog);
                        privateDialog.Messages.Add(message);
                        await messageService.SaveMessageAsync();

                        messageHub.AddDialog(fromUser.Name, privateDialog.Id, fromUser.Avatar32, fromUser.Avatar32);
                        messageHub.AddDialog(toUser.Name, privateDialog.Id, toUser.Avatar32, toUser.Name);
                        //_hubContext.Clients.User(fromUser.Name).addDialog(toUser.Id, toUser.Name, privateDialog.Id);
                        //_hubContext.Clients.User(toUser.Name).addDialog(fromUser.Id, fromUser.Name, privateDialog.Id);

                        //AddDialog(toUser.Name, fromUser.Name, toUser.Id, toUser.Name, privateDialog.Id); 
                    }
                    else
                    {
                        privateDialog.Messages.Add(message);
                        await messageService.SaveMessageAsync();
                    }

                    int newDialogsCount = 0;
                    newDialogsCount = dialogService.UnreadDialogsForUserCount(toUser.Id);

                    messageHub.UpdateMessage(newDialogsCount, toUser.Name);
                    //_hubContext.Clients.User(toUser.Name).updateMessage(newDialogsCount);// hub

                    int messageInDialogCount = 0;
                    messageInDialogCount = dialogService.UnreadMessagesInDialogCount(privateDialog);
                    var lastMessage = privateDialog.Messages.LastOrDefault();
                    if (lastMessage != null)
                    {
                        messageHub.UpdateMessageInDialog(messageInDialogCount, lastMessage.MessageBody, lastMessage.CreatedDate.ToShortDateString(), privateDialog.Id,toUser.Name, toUser.Avatar32, fromUser.Name);
                        //_hubContext.Clients.User(toUser.Name).updateMessageInDialog(toUser.Name, fromUser.Id, fromUser.Name, messageInDialogCount, lastMessage.MessageBody, lastMessage.CreatedDate.ToShortDateString(), privateDialog.Id);
                    }

                    messageHub.AddMessage(toUser.Name, fromUser.Name, message.MessageBody, message.CreatedDate.ToString(), fromUser.Avatar32);

                    //_hubContext.Clients.User(senderName).addMessage(receiverName, senderName, messageBody, date, senderImage);
                    //_hubContext.Clients.User(receiverName).addMessage(receiverName, senderName, messageBody, date, senderImage);
                    return Json(new { success = true });
                }
                return Json(new { success = false, responseText = "Ошибка при отправке сообщения. Повторите попытку" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false, responseText = "Ошибка при отправке сообщения. Повторите попытку" }, JsonRequestBehavior.AllowGet);
        }

        public int GetUnreadDialogsCount()
        {
            int currentUserId = User.Identity.GetUserId<int>();
            int result = 0;
            int dialogsCount = dialogService.UnreadDialogsForUserCount(currentUserId);
            if (dialogsCount != 0)
            {
                result = dialogsCount;
            }

            return result;
        }
    }
}