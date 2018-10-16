using MarketplaceMVC.Data.Autofac;
using MarketplaceMVC.Service.Autofac;
using Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;

using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using MarketplaceMVC.Web.Hangfire;
using Hangfire;
using MarketplaceMVC.Web.SignalrHubs;

namespace MarketplaceMVC.Web
{
    public partial class Startup
    {
        public void ConfigureAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new DataLayer());
            builder.RegisterModule(new ServiceLayer());

            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register(c => app.GetDataProtectionProvider()).InstancePerRequest();

            builder.RegisterType<MessageHub>().As<IMessageHub>().InstancePerRequest();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);


            var container = builder.Build();
            GlobalConfiguration.Configuration.UseActivator(new MarketplaceMVCJobActivator(container));
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
        }
    }
}