﻿using MarketplaceMVC.Web;
using MarketplaceMVC.Web.Areas.User.Automapper;
using MarketplaceMVC.Web.Automapper;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace MarketplaceMVC.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AutoMapperConfiguration.Configure();
            ConfigureAutofac(app);
            ConfigureAuth(app);
        }
    }
}