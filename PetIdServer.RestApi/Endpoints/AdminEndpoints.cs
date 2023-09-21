using AutoMapper;
using Carter;
using MediatR;
using PetIdServer.Application.Requests.Commands.Admin.Login;
using PetIdServer.RestApi.Endpoints.Dto.Admin;

namespace PetIdServer.RestApi.Endpoints;

public class AdminEndpoints : ICarterModule
{
    private const string EndpointBase = "api/admin";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(EndpointBase);

        group.MapPost("login", LoginAdmin).WithName(nameof(LoginAdmin));
    }

    private static async Task<IResult> LoginAdmin(LoginAdminDto dto, ISender sender, IMapper mapper)
    {
        var command = mapper.Map<LoginAdminDto, LoginAdminCommand>(dto);
        var response = await sender.Send(command);

        return Results.Ok(response);
    }
}