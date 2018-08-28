using Autofac;
using MarketplaceMVC.Data.Identity;
using MarketplaceMVC.Data.Infrastructure;
using MarketplaceMVC.Model.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Data.Autofac
{
    public class DataLayer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<UserStore>().As<IUserStore<User, int>>().InstancePerRequest();
            builder.RegisterType<RoleStore>().As<IRoleStore>().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(DataLayer).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();
        }
    }
}
