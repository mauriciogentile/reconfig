using System.Linq;
using Autofac;
using Reconfig.Common.CQRS;
using Reconfig.Common.Diagnostic;
using Reconfig.Domain.Handlers.Commands;
using Reconfig.Web.Infrastructure;
using Module = Autofac.Module;

namespace Reconfig.Web.Modules
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<HeaderProviderFactory>()
                .As<IHeaderProviderFactory>()
                .As<System.Web.Http.ValueProviders.ValueProviderFactory>();

            builder
                .RegisterType<Logger>()
                .AsImplementedInterfaces();
        }
    }
}