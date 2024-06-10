using Carter;
using MediatR;
using PetIdServer.Application.AppDomain.TagDomain.Dto;
using PetIdServer.Application.AppDomain.TagDomain.Queries.ControlCheck;
using PetIdServer.Application.AppDomain.TagDomain.Queries.GetByCode;

namespace PetIdServer.RestApi.Endpoints;

public class TagEndpoints : ICarterModule
{
    private const string EndpointBase = "tag";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(EndpointBase).WithOpenApi();

        group.MapGet("pre-sell/{controlCode:long}", TagControlCheck)
            .WithSummary("Get pre-sell info by control code.")
            .Produces<CheckTagDto>();

        group.MapGet("code/{code}", GetTagByCode)
            .WithSummary("Get Tag by code")
            .WithDescription("Get Tag info by his decoded code in the QR code")
            .Produces<CheckTagDto>();
    }

    private static async Task<IResult> TagControlCheck(long controlCode, ISender sender)
    {
        var query = new TagControlCheckQuery {ControlCode = controlCode};
        var response = await sender.Send(query);

        return Results.Ok(response);
    }

    private static async Task<IResult> GetTagByCode(ISender sender, string code)
    {
        var query = new GetTagByCodeQuery {Code = code};
        var response = await sender.Send(query);

        return Results.Ok(response);
    }
}
