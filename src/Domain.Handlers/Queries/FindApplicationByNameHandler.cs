using System.Linq;
using Reconfig.Common.CQRS;
using Reconfig.Domain.Model;
using Reconfig.Domain.Queries;
using Reconfig.Domain.Repositories;

namespace Reconfig.Domain.Handlers.Queries
{
    public class FindApplicationByNameHandler : IQueryHandler<FindApplicationByName, Application>
    {
        readonly IDomainRepository<Application> _repository;

        public FindApplicationByNameHandler(IDomainRepository<Application> repository)
        {
            _repository = repository;
        }

        public Application Handle(FindApplicationByName param)
        {
            return _repository.Find().FirstOrDefault(x => x.Name == param.ApplicationName);
        }
    }
}
