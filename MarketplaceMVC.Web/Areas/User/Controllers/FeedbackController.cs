using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketplaceMVC.Web.Areas.User.Controllers
{
    public class FeedbackController : Controller
    {
        // GET: User/Feedback
        public ActionResult Index()
        {
            return View();
        }
    }
}