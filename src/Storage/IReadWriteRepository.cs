namespace Reconfig.Storage
{
    public interface IReadWriteRepository<TEntity, in TId, in TExpression> :
        IReadRepository<TEntity, TId>, IWriteRepository<TEntity, TId>
    {
    }
}
