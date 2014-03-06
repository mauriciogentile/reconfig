using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Reconfig.Common.Web;
using Reconfig.Web.Infrastructure;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;

namespace Reconfig.Web
{
    public class AppConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/libs")
                .Include("~/Scripts/lib/jquery-{version}.js")
                .Include("~/Scripts/lib/jquery.unobtrusive*")
                .Include("~/Scripts/lib/jquery.validate*")
                .Include("~/Scripts/lib/bootstrap.js")
                .Include("~/Scripts/lib/bootbox.js")
                .Include("~/Scripts/lib/angular.js")
                .Include("~/Scripts/lib/angular-route.js")
                .Include("~/Scripts/lib/q.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom")
                .Include("~/Scripts/util.js")
                .Include("~/Scripts/app.js")
                .Include("~/Scripts/controllers/*.js")
                .Include("~/Scripts/services/*.js"));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/site.css")
                .Include("~/Content/bootstrap-responsive.css")
                .Include("~/Content/bootstrap.css"));
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional,
                    applicationId = UrlParameter.Optional
                }
            );
        }

        public static void RegisterRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });
        }

        public static void RegisterMvcFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorWithLogAttribute());
            filters.Add(new AuthorizeAttribute());
        }

        public static void RegisterHttpFilters(HttpFilterCollection filters)
        {
            filters.Add(new System.Web.Http.AuthorizeAttribute());
            filters.Add(new ActionFilterWithLogAttribute());
            filters.Add(new ExceptionFilterWithLogAttribute());
            filters.Add(new BadRequestHandleErrorAttribute());
        }
    }
}