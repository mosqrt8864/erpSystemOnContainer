using Autofac.Extensions.DependencyInjection;
using Autofac;
using System.Reflection;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using PurchaseManagement.Application.Commands.CreatePurchaseRequest;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using PurchaseManagement.Application.Mappings;
namespace PurchaseManagement.Api.Infrastructure.AutofacModules;

public class ApplicationModule: Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var configuration = MediatRConfigurationBuilder
                    .Create(typeof(CreatePurchaseRequestCommand).Assembly)
                    .WithAllOpenGenericHandlerTypesRegistered()
                    .Build();
        builder.RegisterMediatR(configuration);
        Console.WriteLine(typeof(MappingProfile).Assembly);
        builder.RegisterAutoMapper(typeof(MappingProfile).Assembly);
    }
}