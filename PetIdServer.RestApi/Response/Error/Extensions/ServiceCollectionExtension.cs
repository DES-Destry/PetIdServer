using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace PetIdServer.RestApi.Response.Error.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServerErrorHandling(this IServiceCollection collection)
    {
        collection.AddTransient<ProblemDetailsFactory, ServerProblemDetailsFactory>();
        collection.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                var factory =
                    context.HttpContext.RequestServices.GetService<ProblemDetailsFactory>();

                context.ProblemDetails = factory == null
                    ? new ProblemDetails()
                    : factory.CreateProblemDetails(context.HttpContext);
            };
        });

        return collection;
    }
}
