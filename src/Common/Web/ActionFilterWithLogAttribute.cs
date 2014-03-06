using System.Web.Http.Filters;
using System.Web.Mvc;
using Reconfig.Common.Diagnostic;

namespace Reconfig.Common.Web
{
    public class ActionFilterWithLogAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response == null)
            {
                return;
            }
            if ((int)actionExecutedContext.Response.StatusCode > 299)
            {
                var logger = DependencyResolver.Current.GetService<ILogger>();
                if (logger != null)
                {
                    logger.Warning(actionExecutedContext.Response.ReasonPhrase);
                }
            }
        }
    }
}
