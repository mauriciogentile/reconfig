using System;

namespace Reconfig.Storage
{
    public interface IWriteRepository<in TEntity, in TId> : IDisposable
    {
        void Save(TEntity entity);
        void Delete(TId id);
    }
}
