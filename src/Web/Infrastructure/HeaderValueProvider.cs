using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http.ValueProviders.Providers;

namespace Reconfig.Web.Infrastructure
{
    public sealed class HeaderValueProvider : NameValuePairsValueProvider
    {
        public HeaderValueProvider(IEnumerable<KeyValuePair<string, string>> values, CultureInfo culture) :
            base(values, culture)
        {
        }

        public HeaderValueProvider(Func<IEnumerable<KeyValuePair<string, string>>> valuesFactory, CultureInfo culture) :
            base(valuesFactory, culture)
        {
        }
    }
}