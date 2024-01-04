using Carter;
using MediatR;
using PetIdServer.Application.Dto.Tag;
using PetIdServer.Application.Requests.Queries.Tag.ControlCheck;

namespace PetIdServer.RestApi.Endpoints;

public class TagEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("tag");

        group.MapGet("pre-sell/{controlCode:long}", TagControlCheck)
            .WithName(nameof(TagControlCheck))
            .WithOpenApi(op =>
            {
                op.Summary = "Get pre-sell info by control code.";
                return op;
            })
            .Produces<CheckTagDto>();
    }

    private static async Task<IResult> TagControlCheck(long controlCode, ISender sender)
    {
        var query = new TagControlCheckQuery {ControlCode = controlCode};
        var response = await sender.Send(query);

        return Results.Ok(response);
    }
}
