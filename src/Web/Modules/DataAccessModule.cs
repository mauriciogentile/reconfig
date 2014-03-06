using System.Configuration;
using Autofac;
using Reconfig.Domain.Repositories;
using Reconfig.Storage;
using Reconfig.Storage.Mongo;

namespace Reconfig.Web.Modules
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var cs = ConfigurationManager.ConnectionStrings["secconfig"];
            if (cs == null)
            {
                throw new ConfigurationErrorsException(
                    "A connection string names \"secconfig\" is missing at configuration file.");
            }

            builder.RegisterGeneric(typeof(DomainRepository<>))
                .As(typeof(IDomainRepository<>))
                .WithParameter("connectionString", cs.ConnectionString);

            builder.RegisterGeneric(typeof(DomainRepository<>))
                .As(typeof(IReadRepository<,>))
                .WithParameter("connectionString", cs.ConnectionString);

            builder.RegisterGeneric(typeof(DomainRepository<>))
                .As(typeof(IWriteRepository<,>))
                .WithParameter("connectionString", cs.ConnectionString);

            builder.RegisterGeneric(typeof(DomainRepository<>))
                .As(typeof(IReadWriteRepository<,,>))
                .WithParameter("connectionString", cs.ConnectionString);
        }
    }
}