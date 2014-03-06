using System.Collections.Generic;
namespace Reconfig.Common.CQRS
{
    public interface IQueryHandler<in TQuery, out TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery param);
    }
}