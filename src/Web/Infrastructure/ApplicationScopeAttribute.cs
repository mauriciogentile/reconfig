using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using Reconfig.Domain.Model;

namespace Reconfig.Web.Infrastructure
{
    public class ApplicationScopeAttribute : ActionFilterAttribute
    {
        ApplicationContext _appContext;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Application application;
            if (!TryGetApplication(filterContext, out application))
            {
                //ajax call => no redirect
                if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Session expired or no Application has been selected");
                else
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Application", action = "Index" }));
            }
            else
            {
                _appContext = DependencyResolver.Current.GetService<ApplicationContext>();
                _appContext.CurrentApplication = application;
            }
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
            var type = filterContext.Result.GetType();
            var prop = type.GetProperty("ApplicationId");
            if (prop != null)
            {
                prop.SetValue(filterContext.Result, _appContext.CurrentApplication.Id, null);
            }
        }

        bool TryGetApplication(ActionExecutingContext filterContext, out Application application)
        {
            application = null;
            if (filterContext.HttpContext.Session != null)
            {
                application = (Application)filterContext.HttpContext.Session["Application"];
                if (application == null)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}