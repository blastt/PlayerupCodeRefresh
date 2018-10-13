using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MarketplaceMVC.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: null,
                url: "account/sell",
                defaults: new { controller = "Offer", action = "Create" },
                namespaces: new[] { "MarketplaceMVC.Web.Controllers" }
            );
            routes.MapRoute(
                name: null,
                url: "accounts/{game}",
                defaults: new { controller = "Offer", action = "List" },
                namespaces: new[] { "MarketplaceMVC.Web.Controllers" }
            );

            routes.MapRoute(
                name: null,
                url: "o/{id}",
                defaults: new { controller = "Offer", action = "Details" },
                namespaces: new[] { "MarketplaceMVC.Web.Controllers" }
            );

            routes.MapRoute(
                name: null,
                url: "u/{name}",
                defaults: new { controller = "UserProfile", action = "Details" },
                namespaces: new[] { "MarketplaceMVC.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" },
                namespaces: new[] { "MarketplaceMVC.Web.Controllers" }
            );


            
        }
    }
}
