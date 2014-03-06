namespace Reconfig.Common.CQRS
{
    public class CommandExecutor : ICommandExecutor
    {
        readonly ICommandHandlerFactory _factory;

        public CommandExecutor(ICommandHandlerFactory factory)
        {
            _factory = factory;
        }

        public void Execute<TCommand>(TCommand command)
        {
            var handler = _factory.Create<TCommand>();
            handler.Handle(command);
        }
    }
}
