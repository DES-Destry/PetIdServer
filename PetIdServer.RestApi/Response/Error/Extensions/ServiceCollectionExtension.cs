using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace PetIdServer.RestApi.Response.Error.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServerErrorHandling(this IServiceCollection collection)
    {
        collection.AddTransient<ProblemDetailsFactory, ServerProblemDetailsFactory>();
        return collection;
    }
}
