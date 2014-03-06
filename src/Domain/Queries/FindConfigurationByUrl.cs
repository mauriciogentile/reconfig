using Reconfig.Common.CQRS;
using Reconfig.Domain.Model;

namespace Reconfig.Domain.Queries
{
    public class FindConfigurationByUrl : IQuery<Configuration>
    {
        public FindConfigurationByUrl(string url)
        {
            Url = url;
        }

        public string Url { get; private set; }
    }
}
