using Reconfig.Storage;

namespace Reconfig.Domain.Repositories
{
    public interface IDomainRepository<TEntity> : IReadWriteRepository<TEntity, string, string>
    {
    }
}