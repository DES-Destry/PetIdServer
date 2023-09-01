using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace PetIdServer.RestApi.Response.Error.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServerErrorHandling(this IServiceCollection collection)
    {
        collection.AddSingleton<ProblemDetails, ServerProblemDetails>();
        collection.AddSingleton<ProblemDetailsFactory, ServerProblemDetailsFactory>();

        return collection;
    }
}