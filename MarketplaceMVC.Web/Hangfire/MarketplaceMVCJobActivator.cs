using Autofac;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketplaceMVC.Web.Hangfire
{
    public class MarketplaceMVCJobActivator : JobActivator
    {
        private IContainer _container;

        public MarketplaceMVCJobActivator(IContainer container)
        {
            _container = container;

        }

        public override object ActivateJob(Type type)
        {
            return _container.Resolve(type);
        }
    }
}