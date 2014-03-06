namespace Reconfig.Common.CQRS
{
    public interface ICommandExecutor
    {
        void Execute<TCommand>(TCommand command);
    }
}
