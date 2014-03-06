using System.Linq;
using Reconfig.Common.CQRS;
using Reconfig.Domain.Model;
using Reconfig.Domain.Queries;
using Reconfig.Domain.Repositories;

namespace Reconfig.Domain.Handlers.Queries
{
    public class FindConfigurationByNameHandler : IQueryHandler<FindConfigurationByName, Configuration>
    {
        readonly IDomainRepository<Configuration> _repository;

        public FindConfigurationByNameHandler(IDomainRepository<Configuration> repository)
        {
            _repository = repository;
        }

        public Configuration Handle(FindConfigurationByName param)
        {
            return _repository.Find()
                .FirstOrDefault(x => x.Name == param.Name && x.ApplicationId == param.ApplicationId);
        }
    }
}
