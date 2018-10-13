using AutoMapper;
using MarketplaceMVC.Model.Models;
using MarketplaceMVC.Service;
using MarketplaceMVC.Service.Identity;
using MarketplaceMVC.Web.Areas.Admin.Models.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MarketplaceMVC.Web.Areas.Admin.Controllers
{
    public class UserProfileController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        private readonly IUserProfileService userProfileService;

        public UserProfileController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ActionResult> List()
        {
            var users = await userProfileService.GetAllUserProfilesAsync(u => u.User);
            var orderedUsers = users.OrderBy(u => u.Name);
            var model = new UserProfileListViewModel()
            {
                UserProfiles = Mapper.Map<IEnumerable<UserProfile>, IEnumerable<UserProfileViewModel>>(orderedUsers.AsEnumerable())
            };
            return View(model);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var user = await userProfileService.GetUserProfileAsync(u => u.Id == id.Value, i => i.DialogsAsCreator, i => i.DialogsAsСompanion, i => i.Billings,
                i => i.FeedbacksMy, i => i.FeedbacksToOthers, i => i.OrdersAsBuyer, i => i.OrdersAsSeller, i => i.OrdersAsMiddleman, i => i.Offers, i => i.MessagesAsReceiver,
                i => i.MessagesAsSender, i => i.Withdraws, i => i.User, i => i.TransactionsAsReceiver, i => i.TransactionsAsSender);
            if (user != null)
            {
                userProfileService.RemoveUserProfile(user);
                await userProfileService.SaveUserProfileAsync();
                return View();
            }
            return HttpNotFound();
        }
        public ActionResult LockUser(int? id)
        {
            if (id != null)
            {
                var model = new LockUserViewModel()
                {
                    UserId = id.Value
                };
                return View(model);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public async Task<ActionResult> LockUser(LockUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }
            var result = await _userManager.SetLockoutEnabledAsync(model.UserId, true);
            TempData["message"] = "Ошибка";
            if (result.Succeeded)
            {


                var user = userProfileService.GetUserProfile(u => u.Id == model.UserId, i => i.User);
                user.LockoutReason = model.LockoutReason;
                await _userManager.SetLockoutEndDateAsync(model.UserId, DateTimeOffset.MaxValue);
                var updateStampResult = await _userManager.UpdateSecurityStampAsync(model.UserId);
                userProfileService.SaveUserProfile();
                if (result.Succeeded && updateStampResult.Succeeded)
                {
                    TempData["message"] = "Пользователь заблокирован";
                }

            }
            return RedirectToAction("List");
        }
        public virtual async Task<ActionResult> UnlockUser(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var user = await userProfileService.GetUserProfileByIdAsync(id.Value);
                       
            if (user != null)
            {
                var result = await _userManager.SetLockoutEnabledAsync(id.Value, false);
                TempData["message"] = "Ошибка";
                if (result.Succeeded)
                {
                    user.LockoutReason = null;
                    await userProfileService.SaveUserProfileAsync();
                    await _userManager.ResetAccessFailedCountAsync(id.Value);
                    TempData["message"] = "Пользователь Разблокирован";
                }
                return RedirectToAction("List");
            }

            return HttpNotFound();
        }


        public async Task<ActionResult> Edit()
        {
            var users = await userProfileService.GetAllUserProfilesAsync(u => u.User);
            var orderedUsers = users.OrderBy(u => u.Name);
            var model = new UserProfileListViewModel()
            {
                UserProfiles = Mapper.Map<IEnumerable<UserProfile>, IEnumerable<UserProfileViewModel>>(orderedUsers.AsEnumerable())
            };
            return View(model);
        }
    }
}