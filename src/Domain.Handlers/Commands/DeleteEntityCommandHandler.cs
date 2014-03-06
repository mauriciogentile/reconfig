using Reconfig.Domain.Commands;
using Reconfig.Domain.Model;
using Reconfig.Domain.Repositories;

namespace Reconfig.Domain.Handlers.Commands
{
    public class DeleteAggregateRootCommandHandler<TRoot> : BaseCommandHandler<DeleteAggregateRoot<TRoot>> where TRoot : AggregateRoot
    {
        readonly IDomainRepository<TRoot> _repo;

        public DeleteAggregateRootCommandHandler(IDomainRepository<TRoot> repo)
        {
            _repo = repo;
        }

        public override void Handle(DeleteAggregateRoot<TRoot> command)
        {
            _repo.Delete(command.Id);
        }
    }
}
