using System;
using Reconfig.Domain.Commands;
using Reconfig.Domain.Model;
using Reconfig.Domain.Repositories;
using Version = Reconfig.Domain.Model.Version;

namespace Reconfig.Domain.Handlers.Commands
{
    public class SaveAggregateRootCommandHandler<TRoot> : BaseCommandHandler<SaveAggregateRoot<TRoot>> where TRoot : AggregateRoot
    {
        readonly IDomainRepository<TRoot> _repo;

        public SaveAggregateRootCommandHandler(IDomainRepository<TRoot> repo)
        {
            _repo = repo;
        }

        public override void Handle(SaveAggregateRoot<TRoot> command)
        {
            var version = command.NewAggregateRoot.Version ?? new Version();
            version.CreatedBy = "mauri";
            version.CreatedOn = DateTime.Now;

            command.NewAggregateRoot.Version = version;

            _repo.Save(command.NewAggregateRoot);
        }
    }
}
