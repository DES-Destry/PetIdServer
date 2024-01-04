using Carter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace PetIdServer.RestApi.Response.Error;

public class ErrorHandlingEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.Map("/error", (
            HttpContext ctx,
            ProblemDetailsFactory? factory,
            string? detail,
            string? instance,
            int? statusCode,
            string? title,
            string? type) =>
        {
            ProblemDetails problemDetails;
            if (factory == null)
                // ProblemDetailsFactory may be null in unit testing scenarios. Improvise to make this more testable.
                problemDetails = new ProblemDetails
                {
                    Detail = detail,
                    Instance = instance,
                    Status = statusCode ?? 500,
                    Title = title,
                    Type = type
                };
            else
                problemDetails = factory.CreateProblemDetails(
                    ctx,
                    statusCode ?? 500,
                    title,
                    type,
                    detail,
                    instance);

            return TypedResults.Problem(problemDetails);
        });
    }
}
