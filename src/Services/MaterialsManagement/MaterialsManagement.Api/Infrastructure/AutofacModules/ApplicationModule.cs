using Autofac.Extensions.DependencyInjection;
using Autofac;
using System.Reflection;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using MaterialsManagement.Application.Commands.CreatePartNumber;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using MaterialsManagement.Application.Mappings;
namespace MaterialsManagement.Api.Infrastructure.AutofacModules;

public class ApplicationModule: Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var configuration = MediatRConfigurationBuilder
                    .Create(typeof(CreatePartNumberCommand).Assembly)
                    .WithAllOpenGenericHandlerTypesRegistered()
                    .Build();
        builder.RegisterMediatR(configuration);
        builder.RegisterAutoMapper(typeof(MappingProfile).Assembly);
    }
}