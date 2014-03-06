using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;

namespace Reconfig.Web.Infrastructure
{
    public sealed class HeaderProviderFactory : ValueProviderFactory, IHeaderProviderFactory
    {
        public override IValueProvider GetValueProvider(HttpActionContext actionContext)
        {
            var headers = actionContext.Request.Headers.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Aggregate((current, next) => current + ";" + next)));
            return new HeaderValueProvider(headers, CultureInfo.InvariantCulture);
        }
    }
}