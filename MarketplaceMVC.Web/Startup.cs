using Hangfire;
using MarketplaceMVC.Web;
using MarketplaceMVC.Web.Areas.User.Automapper;
using MarketplaceMVC.Web.Automapper;
using MarketplaceMVC.Web.Hangfire;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;


[assembly: OwinStartup(typeof(Startup))]
namespace MarketplaceMVC.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureAutofac(app);
            app.MapSignalR();
            AutoMapperConfiguration.Configure();
            MarketplaceMVCHangfire.ConfigureHangfire(app);

            GlobalConfiguration.Configuration
            .UseSqlServerStorage("DefaultConnection");

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ru");
            
            

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() }
            });

            app.UseHangfireServer();
            
        }
    }
}