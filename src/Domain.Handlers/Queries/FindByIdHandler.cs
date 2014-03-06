using Reconfig.Common.CQRS;
using Reconfig.Domain.Model;
using Reconfig.Domain.Queries;
using Reconfig.Storage;

namespace Reconfig.Domain.Handlers.Queries
{
    public class FindByIdHandler<TRoot> : IQueryHandler<FindById<TRoot>, TRoot> where TRoot : AggregateRoot
    {
        readonly IReadRepository<TRoot, string> _repository;

        public FindByIdHandler(IReadRepository<TRoot, string> repository)
        {
            _repository = repository;
        }

        public TRoot Handle(FindById<TRoot> query)
        {
            return _repository.Get(query.Id);
        }
    }
}
