using AutoMapper;
using Carter;
using MediatR;
using PetIdServer.Application.Requests.Commands.Admin.Login;
using PetIdServer.Core.Entities;
using PetIdServer.RestApi.Attributes;
using PetIdServer.RestApi.Auth;
using PetIdServer.RestApi.Binding.Types;
using PetIdServer.RestApi.Endpoints.Dto.Admin;

namespace PetIdServer.RestApi.Endpoints;

public class AdminEndpoints : ICarterModule
{
    private const string EndpointBase = "api/admin";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(EndpointBase);

        group.MapGet("auth", Authenticate).WithName(nameof(Authenticate))
            .RequireAuthorization(AuthSchemas.Admin)
            .WithOpenApi(op =>
        {
            op.Summary = "Get information about admin from token.";
            return op;
        });
        
        group.MapPost("login", LoginAdmin).WithName(nameof(LoginAdmin)).WithOpenApi(op =>
        {
            op.Summary = "Login existed administrator in system.";
            return op;
        });
    }

    private static async Task<IResult> Authenticate([FromAdmin] RequestAdmin admin, ISender sender, IMapper mapper)
    {
        return await Task.FromResult(Results.Ok(admin));
    }

    private static async Task<IResult> LoginAdmin(LoginAdminDto dto, ISender sender, IMapper mapper)
    {
        var command = mapper.Map<LoginAdminDto, LoginAdminCommand>(dto);
        var response = await sender.Send(command);

        return Results.Ok(response);
    }
}