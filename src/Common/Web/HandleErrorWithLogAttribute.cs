using System.Web.Mvc;
using Reconfig.Common.Diagnostic;

namespace Reconfig.Common.Web
{
    public class HandleErrorWithLogAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                var logger = DependencyResolver.Current.GetService<ILogger>();
                if (logger != null)
                {
                    logger.Error(filterContext.Exception.Message, filterContext.Exception);
                }
            }
        }
    }
}
