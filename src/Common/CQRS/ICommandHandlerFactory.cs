namespace Reconfig.Common.CQRS
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<TCommand> Create<TCommand>();
    }
}
