using Carter;
using MediatR;
using PetIdServer.Application.Tags.Dto;
using PetIdServer.Application.Tags.Queries.GetByPublicCode;

namespace PetIdServer.RestApi.Endpoints;

public class TagEndpoints : ICarterModule
{
    private const string EndpointBase = "api/tags";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup(EndpointBase).WithOpenApi();

        group.MapGet("code/{code}", GetTagByCode)
            .WithOpenApi()
            .WithSummary("Get all info for user by code.")
            .Produces<TagDto>();
    }


    private static async Task<IResult> GetTagByCode(string code, ISender sender)
    {
        GetTagByPublicCodeQuery query = new()
        {
            Code = code
        };
        TagDto response = await sender.Send(query);

        return Results.Ok(response);
    }
}
