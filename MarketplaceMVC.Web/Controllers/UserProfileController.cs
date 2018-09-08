using AutoMapper;
using MarketplaceMVC.Model.Models;
using MarketplaceMVC.Service;
using MarketplaceMVC.Web.Models.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MarketplaceMVC.Web.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IUserProfileService userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id != null)
            {
                UserProfile userProfile = await userProfileService.GetUserProfileByIdAsync(id.Value);
                if (userProfile != null)
                {
                    var model = Mapper.Map<UserProfile, UserProfileViewModel>(userProfile);
                    return View(model);
                }
            }
            return HttpNotFound();
        }
    }
}