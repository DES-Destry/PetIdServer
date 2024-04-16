using AutoMapper;
using Carter;
using MediatR;
using PetIdServer.Application.AppDomain.OwnerDomain.Commands.Login;
using PetIdServer.Application.AppDomain.OwnerDomain.Commands.Registration;
using PetIdServer.RestApi.Endpoints.Dto.Owner;

namespace PetIdServer.RestApi.Endpoints;

public class OwnerEndpoints : ICarterModule
{
    private const string EndpointBase = "api/owner";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(EndpointBase).WithOpenApi();

        group.MapPost("", CreateOwner);
        group.MapPost("login", LoginOwner);
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
