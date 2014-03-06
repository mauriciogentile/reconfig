namespace Reconfig.Common.CQRS
{
    public interface ICommandHandler<in TCommand>
    {
        void Handle(TCommand command);
    }
}
