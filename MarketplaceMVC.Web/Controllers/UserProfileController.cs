using AutoMapper;
using MarketplaceMVC.Model.Models;
using MarketplaceMVC.Service;
using MarketplaceMVC.Web.Models.UserProfile;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TinifyAPI;

namespace MarketplaceMVC.Web.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IUserProfileService userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
        }

        public async Task<ActionResult> Details(string name)
        {
            if (name != null)
            {
                UserProfile userProfile = await userProfileService.GetUserProfileByNameAsync(name);
                if (userProfile != null)
                {
                    var model = Mapper.Map<UserProfile, UserProfileViewModel>(userProfile);
                    return View(model);
                }
            }
            return HttpNotFound();
        }

        public string Balance()
        {
            var userId = User.Identity.GetUserId<int>();
            decimal balance = 0;
            UserProfile profile = userProfileService.GetUserProfileById(userId);
            if (profile != null)
            {
                balance = profile.Balance;
            }

            return balance.ToString("C");
        }

        public string Photo(int? id)
        {
            if (id != null)
            {
                // get EF Database
                UserProfile profile = userProfileService.GetUserProfileById(id.Value);
                // find the user. I am skipping validations and other checks.
                if (profile != null)
                {
                    return profile.Avatar32 ?? "";

                }
            }
            return "";
        }

        [HttpGet]
        [Authorize]
        public ActionResult Upload()
        {
            var userId = User.Identity.GetUserId<int>();
            var userProfile = userProfileService.GetUserProfileById(userId);
            if (userProfile != null)
            {
                var model = Mapper.Map<UserProfile, UserProfileViewModel>(userProfile);
                return View(model);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Upload(HttpPostedFileBase image)
        {
            var userId = User.Identity.GetUserId<int>();
            var user = userProfileService.GetUserProfileById(userId);
            if (user != null)
            {
                if (image != null && image.ContentLength <= 1000000 && (image.ContentType == "image/jpeg" || image.ContentType == "image/png"))
                {
                    var guid = Guid.NewGuid();
                    var fileName32 = String.Format(@"{0}-{1}-{2}", guid, "32", Path.GetFileName(image.FileName));
                    var fileName64 = String.Format(@"{0}-{1}-{2}", guid, "64", Path.GetFileName(image.FileName));
                    var fileName96 = String.Format(@"{0}-{1}-{2}", guid, "96", Path.GetFileName(image.FileName));
                    var path32 = Path.Combine(HttpContext.Server.MapPath("~/Content/Images/Avatars/"), fileName32);
                    var path64 = Path.Combine(HttpContext.Server.MapPath("~/Content/Images/Avatars/"), fileName64);
                    var path96 = Path.Combine(HttpContext.Server.MapPath("~/Content/Images/Avatars/"), fileName96);

                    Tinify.Key = ConfigurationManager.AppSettings["TINYPNG_APIKEY"];
                    //Default.png

                    var name32 = user.Avatar32;
                    var name64 = user.Avatar64;
                    var name96 = user.Avatar96;
                    if (name32 != null && name64 != null && name96 != null)
                    {
                        if (name32 != "default32.png" && name64 != "default64.png" && name96 != "default96.png")
                        {
                            System.IO.File.Delete(Path.Combine(HttpContext.Server.MapPath("~/Content/Images/Avatars/"), user.Avatar32));
                            System.IO.File.Delete(Path.Combine(HttpContext.Server.MapPath("~/Content/Images/Avatars/"), user.Avatar64));
                            System.IO.File.Delete(Path.Combine(HttpContext.Server.MapPath("~/Content/Images/Avatars/"), user.Avatar96));
                        }
                    }
                    image.SaveAs(path32);
                    image.SaveAs(path64);
                    image.SaveAs(path96);
                    try
                    {
                        using (var s = Tinify.FromFile(path32))
                        {
                            var resized = s.Resize(new
                            {
                                method = "fit",
                                width = 32,
                                height = 32
                            });
                            await resized.ToFile(path32);
                        }
                        using (var s = Tinify.FromFile(path64))
                        {
                            var resized = s.Resize(new
                            {
                                method = "fit",
                                width = 64,
                                height = 64
                            });
                            await resized.ToFile(path64);
                        }
                        using (var s = Tinify.FromFile(path96))
                        {
                            var resized = s.Resize(new
                            {
                                method = "fit",
                                width = 96,
                                height = 96
                            });
                            await resized.ToFile(path96);
                        }
                    }
                    catch (System.Exception)
                    {
                        // ignored
                    }

                    user.Avatar32 = fileName32;
                    user.Avatar64 = fileName64;
                    user.Avatar96 = fileName96;
                    await userProfileService.SaveUserProfileAsync();

                    return RedirectToAction("Buy", "Offer");



                }

            }
            return HttpNotFound();
        }
    }
}