using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Reconfig.Common.CQRS;
using Reconfig.Common.Exceptions;
using Reconfig.Common.Util;
using Reconfig.Domain.Exceptions;
using Reconfig.Domain.Model;
using Reconfig.Domain.Queries;
using Reconfig.Web.Extensions;

namespace Reconfig.Web.Controllers.Api
{
    public abstract class ApiControllerBase : ApiController
    {
        readonly IQueryHandler<FindApplicationByName, Application> _findAppByName;

        protected ApiControllerBase()
        {
        }

        protected ApiControllerBase(IQueryHandler<FindApplicationByName, Application> findAppByName)
        {
            _findAppByName = findAppByName;
        }

        protected HttpResponseMessage Post(Action action)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState.GetErrors());
            }

            try
            {
                action();
                return Process(HttpStatusCode.Created, (response) => { });
            }
            catch (UnauthorizedAccessException exc)
            {
                var response = Request.CreateResponse(HttpStatusCode.Forbidden);
                response.ReasonPhrase = string.Format("{0} - {1}", HttpStatusCode.Forbidden, exc.Message);
                return response;
            }
        }

        protected Application FindApplication(string applicationName)
        {
            var app = _findAppByName.Handle(new FindApplicationByName(applicationName));
            if (app == null)
            {
                return null;
            }
            return app;
        }

        protected HttpResponseMessage Put(Action action)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState.GetErrors());
            }

            try
            {
                action();
                return Process(HttpStatusCode.Accepted, (response) => { });
            }
            catch (UnauthorizedAccessException exc)
            {
                var response = Request.CreateResponse(HttpStatusCode.Forbidden);
                response.ReasonPhrase = string.Format("{0} - {1}", HttpStatusCode.Forbidden, exc.Message);
                return response;
            }
        }

        protected HttpResponseMessage Delete(Action action)
        {
            return Process(HttpStatusCode.OK, (response) => action());
        }

        HttpResponseMessage Process(HttpStatusCode statusCode, Action<HttpResponseMessage> action)
        {
            var response = Request.CreateResponse(statusCode, string.Empty);
            DoProcess(() => action(response));
            return response;
        }

        protected HttpResponseMessage Get<T>(Func<T> getEntity)
            where T : class
        {
            HttpResponseMessage response = null;

            DoProcess(() =>
            {
                var resource = getEntity();
                if (resource == null)
                {
                    response = new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound };
                    return;
                }

                var content = resource as HttpContent;
                if (content != null)
                {
                    response = new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = content
                    };
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, resource);
                }
            });

            return response;
        }

        protected void DoProcess(Action doProcessAction)
        {
            Guard.Instance.ArgumentNotNull(() => doProcessAction, doProcessAction);

            try
            {
                doProcessAction();
            }
            catch (EntityNotFoundException enfe)
            {
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent(enfe.Message)
                });
            }
            catch (ValidationException ve)
            {
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent(ve.Message)
                });
            }
            catch (ArgumentException ae)
            {
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent(ae.Message)
                });
            }
            catch (Exception e)
            {

                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
#if DEBUG
                    Content = new StringContent(e.ToString())
#else
                    Content = new StringContent("")
#endif
                });
            }
        }
    }

}