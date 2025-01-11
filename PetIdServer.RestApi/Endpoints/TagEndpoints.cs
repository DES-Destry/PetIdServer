using Carter;
using MediatR;
using PetIdServer.Application.Tags.Dto;
using PetIdServer.Application.Tags.Queries.ControlCheck;
using PetIdServer.Application.Tags.Queries.GetByPublicCode;

namespace PetIdServer.RestApi.Endpoints;

public class TagEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("tag");

        group.MapGet("pre-sell/{controlCode:long}", TagControlCheck)
            .WithOpenApi()
            .WithSummary("Get pre-sell info by control code.")
            .Produces<CheckTagDto>();

        group.MapGet("code/{code}", GetTagByCode)
            .WithOpenApi()
            .WithSummary("Get all info for user by code.")
            .Produces<TagDto>();
    }

    private static async Task<IResult> TagControlCheck(long controlCode, ISender sender)
    {
        TagControlCheckQuery query = new()
        {
            ControlCode = controlCode
        };
        CheckTagDto response = await sender.Send(query);

        return Results.Ok(response);
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
