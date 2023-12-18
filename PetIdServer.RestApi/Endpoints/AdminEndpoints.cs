using AutoMapper;
using Carter;
using MediatR;
using PetIdServer.Application.Requests.Commands.Admin.ChangePassword;
using PetIdServer.Application.Requests.Commands.Admin.Login;
using PetIdServer.Application.Requests.Commands.Tag.CreateBatch;
using PetIdServer.Application.Requests.Queries.Tag.GetAll;
using PetIdServer.Application.Requests.Queries.Tag.GetDecoded;
using PetIdServer.RestApi.Auth;
using PetIdServer.RestApi.Binding;
using PetIdServer.RestApi.Endpoints.Dto.Admin;
using PetIdServer.RestApi.Endpoints.EndpointConventions;

namespace PetIdServer.RestApi.Endpoints;

public class AdminEndpoints : ICarterModule
{
    private const string EndpointBase = "api/admin";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(EndpointBase).RequireSecurityKey();

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

        group.MapGet("tag/all", GetAllTags)
            .RequireAuthorization(AuthSchemas.Admin)
            .WithName(nameof(GetAllTags))
            .WithOpenApi(op =>
            {
                op.Summary = "Get all tags with shorted amount of fields.";
                return op;
            });

        group.MapGet("tag/{id:int}", GetDecodedTag)
            .RequireAuthorization(AuthSchemas.Admin)
            .WithName(nameof(GetDecodedTag))
            .WithOpenApi(op =>
            {
                op.Summary = "Get tag with his public code with admin credentials.";
                return op;
            });

        group.MapPost("tags", CreateTags)
            .RequireAuthorization(AuthSchemas.Admin)
            .WithName(nameof(CreateTags))
            .WithOpenApi(op =>
            {
                op.Summary = "Create tags range.";
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

    private static async Task<IResult> GetAllTags(ISender sender)
    {
        var query = new GetAllTagsQuery();
        var response = await sender.Send(query);

        return Results.Ok(response);
    }

    private static async Task<IResult> GetDecodedTag(int id, ISender sender)
    {
        var query = new GetDecodedTagQuery {Id = id};
        var response = await sender.Send(query);

        return Results.Ok(response);
    }

    private static async Task<IResult> CreateTags(CreateTagsDto dto, ISender sender, IMapper mapper)
    {
        var command = mapper.Map<CreateTagsDto, CreateTagsBatchCommand>(dto);
        var response = await sender.Send(command);

        return Results.Ok(response);
    }
}