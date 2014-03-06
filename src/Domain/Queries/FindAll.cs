using System.Collections.Generic;
using Reconfig.Common.CQRS;

namespace Reconfig.Domain.Queries
{
    public class FindAll<TResult> : IQuery<IEnumerable<TResult>>
    {
    }
}
