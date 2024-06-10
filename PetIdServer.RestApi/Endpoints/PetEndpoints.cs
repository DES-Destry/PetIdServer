using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetIdServer.Application.AppDomain.PetDomain.Commands.Attach;
using PetIdServer.Application.AppDomain.PetDomain.Commands.Create;
using PetIdServer.Application.AppDomain.PetDomain.Queries.GetByTagCode;
using PetIdServer.Application.AppDomain.PetDomain.Queries.GetByTagId;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Common.Services.Dto;
using PetIdServer.Core.Domain.Owner;
using PetIdServer.RestApi.Auth;
using PetIdServer.RestApi.Binding;
using PetIdServer.RestApi.Endpoints.Dto.Pet;

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

        group.MapPost("", CreatePet)
            .RequireAuthorization(AuthSchemas.Owner)
            .WithSummary("Create new pet")
            .WithDescription("Create new pet for further attaching to the Tag")
            .Produces<VoidResponseDto>();

        group.MapPost("attach", AttachPet)
            .RequireAuthorization(AuthSchemas.Owner)
            .WithSummary("Attach pet")
            .WithDescription("Attach a pet to the Tag")
            .Produces<VoidResponseDto>();
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

    private static async Task<IResult> CreatePet(
        ISender sender,
        IMapper mapper,
        RequestOwner owner,
        [FromBody] CreatePetDto body)
    {
        var command = new CreatePetCommand
        {
            Name = body.Name,
            Type = body.Type,
            Sex = body.Sex,
            IsCastrated = body.IsCastrated,
            Owner = mapper.Map<RequestOwner, OwnerEntity>(owner)
        };
        var response = await sender.Send(command);

        return Results.Ok(response);
    }

    private static async Task<IResult> AttachPet(
        ISender sender,
        IMapper mapper,
        [FromBody] AttachPetDto body)
    {
        var command = mapper.Map<AttachPetDto, AttachPetCommand>(body);
        var response = await sender.Send(command);

        return Results.Ok(response);
    }
}
