using System.Collections.Generic;
using Reconfig.Common.CQRS;
using Reconfig.Domain.Model;

namespace Reconfig.Domain.Queries
{
    public class FindByApplicationId<T> : IQuery<IEnumerable<T>> where T : IFromApplication
    {
        public FindByApplicationId(string applicationId)
        {
            ApplicationId = applicationId;
        }

        public string ApplicationId { get; private set; }
    }
}
