using Carter;
using MediatR;
using PetIdServer.Application.AppDomain.TagDomain.Dto;
using PetIdServer.Application.AppDomain.TagDomain.Queries.ControlCheck;

namespace PetIdServer.RestApi.Endpoints;

public class TagEndpoints : ICarterModule
{
    private const string EndpointBase = "tag";
    
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(EndpointBase);

        group.MapGet("pre-sell/{controlCode:long}", TagControlCheck)
            .WithOpenApi()
            .WithSummary("Get pre-sell info by control code.")
            .Produces<CheckTagDto>();
    }

    private static async Task<IResult> TagControlCheck(long controlCode, ISender sender)
    {
        var query = new TagControlCheckQuery {ControlCode = controlCode};
        var response = await sender.Send(query);

        return Results.Ok(response);
    }
}
