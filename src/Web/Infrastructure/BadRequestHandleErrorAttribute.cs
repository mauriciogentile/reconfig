using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Reconfig.Domain.Exceptions;

namespace Reconfig.Web.Infrastructure
{
    class BadRequestHandleErrorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var excType = actionExecutedContext.Exception.GetType();
            if (excType == typeof(TemplateNotFoundException) || excType == typeof(ParsingException))
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionExecutedContext.Exception.Message);
            }
            else if (excType == typeof(UnauthorizedAccessException))
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden, actionExecutedContext.Exception.Message);
            }
        }
    }
}
