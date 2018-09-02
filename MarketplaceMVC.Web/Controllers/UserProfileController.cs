using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketplaceMVC.Web.Controllers
{
    public class UserProfileController : Controller
    {
        // GET: UserProfile
        public ActionResult Create()
        {
            return View();
        }
    }
}