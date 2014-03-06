using System;
using Reconfig.Common.CQRS;
using Reconfig.Domain.Model;

namespace Reconfig.Domain.Queries
{
    public class FindConfigurationByName : IQuery<Configuration>
    {
        public FindConfigurationByName(string name, string appId)
        {
            Name = name;
            ApplicationId = appId;
        }

        public string Name { get; private set; }
        public string ApplicationId { get; private set; }
    }
}
