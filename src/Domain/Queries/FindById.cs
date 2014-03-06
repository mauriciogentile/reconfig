using Reconfig.Common.CQRS;

namespace Reconfig.Domain.Queries
{
    public class FindById<TResult> : IQuery<TResult>
    {
        public FindById(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }
}
