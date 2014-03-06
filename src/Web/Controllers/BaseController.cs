using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Reconfig.Web.Extensions;

namespace Reconfig.Web.Controllers
{
    public class BaseController : Controller
    {
        /*        protected ActionResult ProcessPost(Func<Uri> action)
                {
                }

                protected HttpResponseMessage ProcessPut(Action action)
                {
                    if (!ModelState.IsValid)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState.GetErrors());
                    }

                    return Process(HttpStatusCode.NoContent, (response) => action());
                }

                protected HttpResponseMessage ProcessDelete(Action action)
                {
                    return Process(HttpStatusCode.NoContent, (response) => action());
                }

                HttpResponseMessage Process(HttpStatusCode statusCode, Action<HttpResponseMessage> action)
                {
                    var response = Request.CreateResponse(statusCode, string.Empty);
                    DoProcess(() => action(response));
                    return response;
                }

                protected HttpResponseMessage ProcessGet<T>(Func<T> getEntity)
                    where T : class
                {
                    HttpResponseMessage response = null;

                    DoProcess(() =>
                    {
                        var resource = getEntity();
                        if (resource == null)
                        {
                            response = new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound };
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
                }*/

    }
}
