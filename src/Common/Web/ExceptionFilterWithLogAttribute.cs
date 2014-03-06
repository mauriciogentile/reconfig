using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Mvc;
using Reconfig.Common.Diagnostic;

namespace Reconfig.Common.Web
{
    public class ExceptionFilterWithLogAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var logger = DependencyResolver.Current.GetService<ILogger>();
            if (logger != null)
            {
                logger.Error(actionExecutedContext.Exception.Message, actionExecutedContext.Exception);
            }

            actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, actionExecutedContext.Exception.Message);
        }
    }
}
