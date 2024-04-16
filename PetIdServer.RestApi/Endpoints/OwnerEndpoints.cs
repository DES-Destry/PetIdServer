using AutoMapper;
using Carter;
using MediatR;
using PetIdServer.Application.AppDomain.OwnerDomain.Commands.Login;
using PetIdServer.Application.AppDomain.OwnerDomain.Commands.Registration;
using PetIdServer.Application.Common.Dto;
using PetIdServer.RestApi.Endpoints.Dto.Owner;

namespace PetIdServer.RestApi.Endpoints;

public class OwnerEndpoints : ICarterModule
{
    private const string EndpointBase = "api/owner";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(EndpointBase).WithOpenApi();

        group.MapPost("", CreateOwner)
            .WithSummary("Create new account.")
            .WithDescription(
                "Create new account of owner and get pair of tokens that will able to create new pets.")
            .Produces<TokenPairDto>();
        group.MapPost("login", LoginOwner)
            .WithSummary("Login as owner.")
            .WithDescription("Get new token pairs with owner's creds.")
            .Produces<LoginOwnerResponseDto>();
        ;
    }

    private static async Task<IResult> CreateOwner(
        CreateOwnerDto dto,
        ISender sender,
        IMapper mapper)
    {
        var command = mapper.Map<CreateOwnerDto, RegistrationOwnerCommand>(dto);
        var response = await sender.Send(command);

        return Results.Ok(response);
    }

    private static async Task<IResult> LoginOwner(LoginOwnerDto dto, ISender sender, IMapper mapper)
    {
        var command = mapper.Map<LoginOwnerDto, LoginOwnerCommand>(dto);
        var response = await sender.Send(command);

        return Results.Ok(response);
    }
}
