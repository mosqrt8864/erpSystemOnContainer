using Autofac.Extensions.DependencyInjection;
using Autofac;
using PurchaseManagement.Domain.Interfaces;
using PurchaseManagement.Infrastructure.Repositories;
using PurchaseManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace PurchaseManagement.Api.Infrastructure.AutofacModules;

public class InfrastructureModule: Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<PurchaseRequestRepository>()
            .As<IPurchaseRequestRepository>()
            .InstancePerLifetimeScope();
        builder
        .RegisterType<PurchaseContext>()
        .WithParameter("options", new DbContextOptionsBuilder<PurchaseContext>()
        .UseInMemoryDatabase("ERPSystemDb").Options)
        .InstancePerLifetimeScope();
    }
}
