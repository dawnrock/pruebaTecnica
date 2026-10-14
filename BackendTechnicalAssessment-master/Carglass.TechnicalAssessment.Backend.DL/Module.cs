using Autofac;
using Carglass.TechnicalAssessment.Backend.DL.Repositories;
using Carglass.TechnicalAssessment.Backend.Entities;

namespace Carglass.TechnicalAssessment.Backend.DL;

public class Module : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterRepositories(builder);
    }

    private static void RegisterRepositories(ContainerBuilder builder)
    {
        builder.RegisterType<ClientIMRepository>()
            .As<ICrudRepository<Client>>()
            .SingleInstance();

        builder.RegisterType<ProductRepository>()
            .As<ICrudRepository<Product>>()
            .SingleInstance();
    }
}