using System.Linq;
using Reconfig.Common.CQRS;
using Reconfig.Domain.Model;
using Reconfig.Domain.Queries;
using Reconfig.Domain.Repositories;

namespace Reconfig.Domain.Handlers.Queries
{
    public class FindConfigurationByUrlHandler : IQueryHandler<FindConfigurationByUrl, Configuration>
    {
        readonly IDomainRepository<Configuration> _repository;

        public FindConfigurationByUrlHandler(IDomainRepository<Configuration> repository)
        {
            _repository = repository;
        }

        public Configuration Handle(FindConfigurationByUrl param)
        {
            var url = param.Url.ToLower().Trim();
            return _repository.Find().ToList().FirstOrDefault(x => x.Url.ToLower().Trim().Equals(url));
        }
    }
}
