using Autofac.Extensions.DependencyInjection;
using Autofac;
using MaterialsManagement.Infrastructure;
using MaterialsManagement.Domain.Interfaces;
using MaterialsManagement.Infrastructure.Repositories; 
using Microsoft.EntityFrameworkCore;

namespace MaterialsManagement.Api.Infrastructure.AutofacModules;

public class InfrastructureModule: Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<PartNumberRepository>()
            .As<IPartNumberRepository>()
            .InstancePerLifetimeScope();
        builder
        .RegisterType<MaterialsContext>()
        .WithParameter("options", new DbContextOptionsBuilder<MaterialsContext>()
        .UseInMemoryDatabase("ERPSystemDb").Options)
        .InstancePerLifetimeScope();
    }
}
