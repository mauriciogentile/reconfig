using System.Collections.Generic;
using System.Linq;
using Reconfig.Common.CQRS;
using Reconfig.Domain.Model;
using Reconfig.Domain.Queries;
using Reconfig.Domain.Repositories;

namespace Reconfig.Domain.Handlers.Queries
{
    public class FindByApplicationIdHandler<T> : IQueryHandler<FindByApplicationId<T>, IEnumerable<T>> where T : IFromApplication
    {
        readonly IDomainRepository<T> _repository;

        public FindByApplicationIdHandler(IDomainRepository<T> repository)
        {
            _repository = repository;
        }

        #region IQueryHandler<FindByApplicationId<T>,IEnumerable<T>> Members

        public IEnumerable<T> Handle(FindByApplicationId<T> param)
        {
            return _repository.Find().Where(x => x.ApplicationId == param.ApplicationId);
        }

        #endregion
    }
}
