using System.Linq;
using Autofac;
using Reconfig.Common.CQRS;
using Reconfig.Domain.Handlers.Queries;

namespace Reconfig.Web.Modules
{
    public class QueriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterGeneric(typeof(FindByIdHandler<>))
                .As(typeof(IQueryHandler<,>))
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(FindByApplicationIdHandler<>))
               .As(typeof(IQueryHandler<,>))
               .InstancePerLifetimeScope();
            
            builder.RegisterGeneric(typeof(FindAllHandler<>))
                .As(typeof(IQueryHandler<,>))
                .InstancePerLifetimeScope();

            builder.RegisterTypes(typeof(FindConfigurationByUrlHandler).Assembly.GetTypes()
                .Where(t => t.Namespace == typeof(FindConfigurationByUrlHandler).Namespace).ToArray())
                .Where(t => !t.IsGenericType)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}