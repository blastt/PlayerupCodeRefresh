﻿using System.Web.Mvc;

namespace MarketplaceMVC.Web.Areas.User
{
    public class UserAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "User";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {

            context.MapRoute(
                "User_default",
                "User/{controller}/{action}",
                new { controller = "Dialog",action = "Inbox" },
                namespaces: new[] { "MarketplaceMVC.Web.Areas.User.Controllers" }
            );
        }
    }
}