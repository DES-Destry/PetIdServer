using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PetIdServer.Application.Mapper;

namespace PetIdServer.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}