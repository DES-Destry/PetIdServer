using AutoMapper;
using Carter;
using MediatR;
using PetIdServer.Application.Requests.Commands.Admin.ChangePassword;
using PetIdServer.Application.Requests.Commands.Admin.Login;
using PetIdServer.RestApi.Auth;
using PetIdServer.RestApi.Binding;
using PetIdServer.RestApi.Endpoints.Dto.Admin;

namespace PetIdServer.RestApi.Endpoints;

public class AdminEndpoints : ICarterModule
{
    private const string EndpointBase = "api/admin";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(EndpointBase).RequireAuthorization();

        group.MapGet("auth", Authenticate)
            .RequireAuthorization(AuthSchemas.Admin)
            .WithName(nameof(Authenticate))
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

        group.MapPut("password", ChangePassword)
            .RequireAuthorization(AuthSchemas.Admin)
            .WithName(nameof(ChangePassword))
            .WithOpenApi(op =>
        {
            op.Summary = "Change password as authenticated admin.";
            return op;
        });
    }
    
    private static async Task<IResult> Authenticate(RequestAdmin admin, ISender sender, IMapper mapper)
    {
        return await Task.FromResult(Results.Ok(admin));
    }

    private static async Task<IResult> LoginAdmin(LoginAdminDto dto, ISender sender, IMapper mapper)
    {
        var command = mapper.Map<LoginAdminDto, LoginAdminCommand>(dto);
        var response = await sender.Send(command);

        return Results.Ok(response);
    }

    private static async Task<IResult> ChangePassword(RequestAdmin admin, ChangePasswordDto dto, ISender sender)
    {
        var command = new ChangePasswordCommand
            {Id = admin.Username, OldPassword = dto.OldPassword, NewPassword = dto.NewPassword};
        var response = await sender.Send(command);

        return Results.Ok(response);
    }
}