using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetIdServer.Application.AppDomain.AdminDomain.Commands.ChangePassword;
using PetIdServer.Application.AppDomain.AdminDomain.Commands.Login;
using PetIdServer.Application.AppDomain.TagDomain.Commands.Clear;
using PetIdServer.Application.AppDomain.TagDomain.Commands.CreateBatch;
using PetIdServer.Application.AppDomain.TagDomain.Queries.GetAll;
using PetIdServer.Application.AppDomain.TagDomain.Queries.GetDecoded;
using PetIdServer.Application.AppDomain.TagReportDomain.Commands.Create;
using PetIdServer.Application.AppDomain.TagReportDomain.Commands.Resolve;
using PetIdServer.Application.AppDomain.TagReportDomain.Dto;
using PetIdServer.Application.AppDomain.TagReportDomain.Queries.GetAll;
using PetIdServer.Application.Common.Dto;
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

        group.MapPost("tag/{id:int}/clear", ClearTag)
            .RequireAuthorization(AuthSchemas.Admin)
            .WithName(nameof(ClearTag))
            .WithOpenApi(op =>
            {
                op.Summary = "Remove the pet from tag force with admin permissions";
                return op;
            })
            .Produces<VoidResponseDto>();

        group.MapGet("report/tag/all", GetAllTagReports)
            .RequireAuthorization(AuthSchemas.Admin)
            .WithName(nameof(GetAllTagReports))
            .WithSummary("Get all retorts with abused tags with filters(isResolved, tagId)")
            .Produces<TagReportsDto>();

        group.MapPost("report/tag/{id:int}", CreateTagReport)
            .WithName(nameof(CreateTagReport))
            .WithSummary("Create report that by opinion of admin was abused.")
            .RequireAuthorization(AuthSchemas.Admin)
            .Produces<VoidResponseDto>();

        group.MapPost("report/{id:guid}/resolve", ResolveTagReport)
            .WithName(nameof(ResolveTagReport))
            .WithSummary(
                "To not pay attention for already resolved reports it must be marked as resolved")
            .RequireAuthorization(AuthSchemas.Admin)
            .Produces<VoidResponseDto>();
    }

    private static async Task<IResult> Authenticate(
        RequestAdmin admin,
        ISender sender,
        IMapper mapper) => await Task.FromResult(Results.Ok(admin));

    private static async Task<IResult> LoginAdmin(LoginAdminDto dto, ISender sender, IMapper mapper)
    {
        var command = mapper.Map<LoginAdminDto, LoginAdminCommand>(dto);
        var response = await sender.Send(command);

        return Results.Ok(response);
    }

    private static async Task<IResult> ChangePassword(
        RequestAdmin admin,
        ChangePasswordDto dto,
        ISender sender)
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

    private static async Task<IResult> ClearTag(int id, RequestAdmin admin, ISender sender)
    {
        var command = new ClearTagCommand {AdminId = admin.Username, TagId = id};
        var response = await sender.Send(command);

        return Results.Ok(response);
    }

    private static async Task<IResult> GetAllTagReports(
        [FromQuery] int? tagId,
        [FromQuery] bool? isResolved,
        ISender sender)
    {
        var query = new GetAllTagReportsQuery
        {
            TagId = tagId,
            IsResolved = isResolved
        };
        var response = await sender.Send(query);

        return Results.Ok(response);
    }

    private static async Task<IResult> CreateTagReport(int id, RequestAdmin admin, ISender sender)
    {
        var command = new CreateTagReportCommand
        {
            AdminId = admin.Username,
            TagId = id
        };
        var response = await sender.Send(command);

        return Results.Ok(response);
    }

    private static async Task<IResult> ResolveTagReport(Guid id, RequestAdmin admin, ISender sender)
    {
        var command = new ResolveTagReportCommand
        {
            AdminId = admin.Username,
            ReportId = id
        };
        var response = await sender.Send(command);

        return Results.Ok(response);
    }
}
