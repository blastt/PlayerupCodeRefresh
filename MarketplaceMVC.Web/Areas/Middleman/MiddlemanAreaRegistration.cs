using System.Web.Mvc;

namespace MarketplaceMVC.Web.Areas.Middleman
{
    public class MiddlemanAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Middleman";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Middleman_default",
                "Middleman/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}