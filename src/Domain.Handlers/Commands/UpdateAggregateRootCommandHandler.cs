using System;
using Reconfig.Domain.Commands;
using Reconfig.Domain.Model;
using Reconfig.Domain.Repositories;
using Version = Reconfig.Domain.Model.Version;

namespace Reconfig.Domain.Handlers.Commands
{
    public class UpdateAggregateRootCommandHandler<TRoot> : BaseCommandHandler<UpdateAggregateRoot<TRoot>> where TRoot : AggregateRoot
    {
        readonly IDomainRepository<TRoot> _repo;

        public UpdateAggregateRootCommandHandler(IDomainRepository<TRoot> repo)
        {
            _repo = repo;
        }

        public override void Handle(UpdateAggregateRoot<TRoot> command)
        {
            if (command.UpdatedAggregateRoot.Id == null)
            {
                return;
            }

            var version = command.UpdatedAggregateRoot.Version ?? new Version();
            version.LastUpdatedBy = "mauri";
            version.LastUpdatedOn = DateTime.Now;

            command.UpdatedAggregateRoot.Version = version;

            _repo.Save(command.UpdatedAggregateRoot);
        }
    }
}
