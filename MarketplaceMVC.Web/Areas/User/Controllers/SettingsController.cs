using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketplaceMVC.Web.Areas.User.Controllers
{
    public class SettingsController : Controller
    {
        // GET: User/Settings
        public ActionResult Index()
        {
            return View();
        }
    }
}