using System.Collections.Generic;
using System.Linq;

namespace Reconfig.Storage
{
    public interface IReadRepository<out TEntity, in TId>
    {
        IQueryable<TEntity> Find();
        IEnumerable<TEntity> GetAll();
        TEntity Get(TId id);
    }
}
