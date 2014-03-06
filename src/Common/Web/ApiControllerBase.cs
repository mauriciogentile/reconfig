using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Reconfig.Common.Web
{
    public class ApiControllerBase : ApiController
    {
        protected HttpResponseMessage Get<T>(Func<T> action, string errorMessage = null)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, action());
            }
            catch (Exception exc)
            {
                throw new ApplicationException(errorMessage ?? exc.Message, exc);
            }
        }

        protected HttpResponseMessage Post<T>(Action action, string errorMessage = null)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrors(ModelState));
            }
            try
            {
                action();
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception exc)
            {
                throw new ApplicationException(errorMessage ?? exc.Message, exc);
            }
        }

        static string GetErrors(ModelStateDictionary modalState)
        {
            var errorsSb = new StringBuilder();
            var errors = modalState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage)).ToList();
            errors.ForEach(x => errorsSb.AppendLine(x));
            return errorsSb.ToString();
        }
    }
}