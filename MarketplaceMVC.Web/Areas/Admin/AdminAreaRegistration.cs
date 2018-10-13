using System.Web.Mvc;

namespace MarketplaceMVC.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {

            context.MapRoute(
                "Admin_default1",
                "Admin/{controller}/{action}",
                new { action = "Create", controller = "Game" }
            );
        }
    }
}