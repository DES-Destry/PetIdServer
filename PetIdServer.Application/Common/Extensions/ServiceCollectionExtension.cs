using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PetIdServer.Application.Common.Mapper;

namespace PetIdServer.Application.Common.Extensions;

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
