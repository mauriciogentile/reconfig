using System;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Reconfig.Web.Controllers;

namespace Reconfig.Web.Modules
{
    public class ControllersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterTypes()
                .Where(t => t.IsAssignableFrom(typeof(Controller)))
                .Where(t => t.IsAssignableFrom(typeof(ApiController)))
                .AsSelf()
                .AsImplementedInterfaces();
        }
    }
}