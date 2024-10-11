using Microsoft.Extensions.DependencyInjection;
using PetIdServer.Infrastructure.Extensions;

namespace PetIdServer.Tests.Extensions;

public static class TestDependencies
{
    public static T GetRequiredService<T>() where T : notnull
    {
        var deps = ConfigureDependencies();
        return deps.GetRequiredService<T>();
    }

    private static ServiceProvider ConfigureDependencies()
    {
        ServiceCollection services = [];

        services.AddInfrastructureServices();

        return services.BuildServiceProvider();
    }
}
