using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

namespace Reconfig.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            AppConfig.RegisterRoutes(GlobalConfiguration.Configuration);
            AppConfig.RegisterMvcFilters(GlobalFilters.Filters);
            AppConfig.RegisterHttpFilters(GlobalConfiguration.Configuration.Filters);
            AppConfig.RegisterRoutes(RouteTable.Routes);
            AppConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureIoCContainer();
        }

        static void ConfigureIoCContainer()
        {
            var builder = new ContainerBuilder();

            typeof(MvcApplication).Assembly
                          .GetTypes()
                          .Where(t => typeof(Autofac.Module).IsAssignableFrom(t) && !t.IsAbstract)
                          .Select(Activator.CreateInstance).OfType<Autofac.Module>()
                          .ToList().ForEach(builder.RegisterModule);

            

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}