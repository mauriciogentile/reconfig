using System.Collections.Generic;
using Reconfig.Common.CQRS;
using Reconfig.Domain.Model;
using Reconfig.Domain.Queries;
using Reconfig.Storage;

namespace Reconfig.Domain.Handlers.Queries
{
    public class FindAllHandler<TRoot> : IQueryHandler<FindAll<TRoot>, IEnumerable<TRoot>> where TRoot : AggregateRoot
    {
        readonly IReadRepository<TRoot, string> _repository;

        public FindAllHandler(IReadRepository<TRoot, string> repository)
        {
            _repository = repository;
        }

        public IEnumerable<TRoot> Handle(FindAll<TRoot> query)
        {
            return _repository.GetAll();
        }
    }
}
