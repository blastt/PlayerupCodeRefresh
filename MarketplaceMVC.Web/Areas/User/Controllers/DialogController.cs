using AutoMapper;
using MarketplaceMVC.Model.Models;
using MarketplaceMVC.Service;
using MarketplaceMVC.Web.Areas.User.Models.Dialog;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MarketplaceMVC.Web.Areas.User.Controllers
{
    [Authorize]
    public class DialogController : Controller
    {
        private readonly IDialogService dialogService;
        private readonly IUserProfileService userProfileService;
        // GET: Offer
        public DialogController(IDialogService dialogService, IUserProfileService userProfileService)
        {
            this.dialogService = dialogService;
            this.userProfileService = userProfileService;
        }

        public async Task<ActionResult> Inbox()
        {
            int currentUserId = User.Identity.GetUserId<int>();
            var dialogs = await dialogService.GetUserDialogsAsync(currentUserId);
            var modelDialogs = Mapper.Map<IEnumerable<Dialog>, IEnumerable<DialogViewModel>>(dialogs);
            var model = new DialogListViewModel()
            {
                Dialogs = modelDialogs
            };
            return View(model);
        }

        public async Task<ViewResult> Unread()
        {

            int currentUserId = User.Identity.GetUserId<int>();
            var dialogs = await dialogService.GetUserDialogsAsync(currentUserId, d => d.Messages.Any(m => !m.ToViewed));
            var modelDialogs = Mapper.Map<IEnumerable<Dialog>, IEnumerable<DialogViewModel>>(dialogs);
            var model = new DialogListViewModel()
            {
                Dialogs = modelDialogs
            };
            return View(model);
        }

        public async Task<ViewResult> Details(int? id)
        {
            if (id != null)
            {
                var dialog = await dialogService.GetDialogAsync(id.Value);
                if (dialog != null)
                {
                    var model = Mapper.Map<Dialog, DetailsDialogViewModel>(dialog);
                    return View(model);
                }               
            }
            return View();
        }

        
    }
}