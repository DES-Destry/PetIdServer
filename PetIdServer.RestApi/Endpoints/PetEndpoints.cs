using Carter;
using MediatR;
using PetIdServer.Application.AppDomain.PetDomain.Dto;
using PetIdServer.Application.AppDomain.PetDomain.Queries.GetByTagCode;
using PetIdServer.Application.AppDomain.PetDomain.Queries.GetByTagId;

namespace PetIdServer.RestApi.Endpoints;

public class PetEndpoints : ICarterModule
{
    private const string EndpointBase = "pet";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(EndpointBase).WithOpenApi();

        group.MapGet("tag-id/{id:int}", GetByTagId)
            .WithSummary("Get pet by his Tag Id")
            .WithDescription("Get pet by his 4-digit Tag Id like 0489")
            .Produces<PetDto>();

        group.MapGet("tag-code/{code}", GetByTagCode)
            .WithSummary("Get pet by his Tag code")
            .WithDescription("Get pet by his unreadable Tag code that encoded in the QR code")
            .Produces<PetDto>();
    }

    private static async Task<IResult> GetByTagId(ISender sender, int id)
    {
        var query = new GetPetByTagIdQuery {TagId = id};
        var response = await sender.Send(query);

        return Results.Ok(response);
    }

    private static async Task<IResult> GetByTagCode(ISender sender, string code)
    {
        var query = new GetPetByTagCodeQuery {Code = code};
        var response = await sender.Send(query);

        return Results.Ok(response);
    }
}
