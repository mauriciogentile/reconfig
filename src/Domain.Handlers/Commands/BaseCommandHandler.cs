using Reconfig.Common.CQRS;

namespace Reconfig.Domain.Handlers.Commands
{
    public abstract class BaseCommandHandler<TCommand> : ICommandHandler<TCommand> 
    {
        public virtual void Handle(TCommand command)
        {
        }
    }
}
