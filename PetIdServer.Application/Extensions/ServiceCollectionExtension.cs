using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PetIdServer.Application.Mapper;

namespace PetIdServer.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(ServiceCollectionExtension).GetTypeInfo().Assembly);
        services.AddAutoMapper(typeof(MappingProfile));
        
        return services;
    }
}