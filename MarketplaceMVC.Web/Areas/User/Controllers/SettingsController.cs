using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketplaceMVC.Web.Areas.User.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}