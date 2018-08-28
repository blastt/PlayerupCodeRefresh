using Autofac;
using MarketplaceMVC.Model.Models;
using MarketplaceMVC.Service.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Service.Autofac
{
    public class ServiceLayer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => ApplicationUserManager.Create(c.Resolve<IUserStore<User, int>>(), c.Resolve<IDataProtectionProvider>())).AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationRoleManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
        }
    }
}
