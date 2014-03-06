using System.Linq;
using Autofac;
using Reconfig.Common.CQRS;
using Reconfig.Domain.Handlers.Commands;
using Module = Autofac.Module;

namespace Reconfig.Web.Modules
{
    public class CommandsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterGeneric(typeof(UpdateAggregateRootCommandHandler<>))
                .As(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(DeleteAggregateRootCommandHandler<>))
                .As(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(SaveAggregateRootCommandHandler<>))
                .As(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();
        }
    }
}