using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetIdServer.Application.AppDomain.AdminDomain.Commands.ChangePassword;
using PetIdServer.Application.AppDomain.AdminDomain.Commands.Login;
using PetIdServer.Application.AppDomain.TagDomain.Commands.Clear;
using PetIdServer.Application.AppDomain.TagDomain.Commands.CreateBatch;
using PetIdServer.Application.AppDomain.TagDomain.Dto;
using PetIdServer.Application.AppDomain.TagDomain.Queries.GetAll;
using PetIdServer.Application.AppDomain.TagDomain.Queries.GetDecoded;
using PetIdServer.Application.AppDomain.TagReportDomain.Commands.Create;
using PetIdServer.Application.AppDomain.TagReportDomain.Commands.Resolve;
using PetIdServer.Application.AppDomain.TagReportDomain.Dto;
using PetIdServer.Application.AppDomain.TagReportDomain.Queries.GetAll;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Common.Services.Dto;
using PetIdServer.RestApi.Auth;
using PetIdServer.RestApi.Binding;
using PetIdServer.RestApi.Endpoints.Dto.Admin;
using PetIdServer.RestApi.Endpoints.EndpointConventions;

namespace PetIdServer.RestApi.Endpoints;

public class AdminEndpoints : ICarterModule
{
    private const string EndpointBase = "admin";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(EndpointBase).RequireSecurityKey().WithOpenApi();
        // var authorizedGroup = group.RequireAuthorization(AuthSchemas.Admin);

        group.MapGet("auth", Authenticate)
            .RequireAuthorization(AuthSchemas.Admin)
            .WithSummary("Get information about admin from token (admin).")
            .Produces<AdminDto>();

        group.MapPost("login", LoginAdmin)
            .WithSummary("Login existed administrator in system (admin).")
            .Produces<LoginAdminResponseDto>();

        group.MapPut("password", ChangePassword)
            .RequireAuthorization(AuthSchemas.Admin)
            .WithSummary("Change password as authenticated admin.")
            .Produces<SingleTokenDto>();

        group.MapGet("tags", GetAllTags)
            .RequireAuthorization(AuthSchemas.Admin)
            .WithSummary("Get all tags (admin).")
            .WithDescription("Get all tags with shorted amount of fields.")
            .Produces<TagReviewList>();

        group.MapGet("tags/{id:int}", GetDecodedTag)
            .RequireAuthorization(AuthSchemas.Admin)
            .WithSummary("Get decoded tag (admin).")
            .WithDescription("Get tag with his public code with admin credentials.")
            .Produces<TagForAdminDto>();

        group.MapPost("tags", CreateTags)
            .RequireAuthorization(AuthSchemas.Admin)
            .WithSummary("Create tags range (admin).")
            .WithDescription(
                "Describe range and it will create all tags from x to y. Be careful with conflicts!")
            .Produces<VoidResponseDto>();

        group.MapPost("tags/{id:int}/clear", ClearTag)
            .RequireAuthorization(AuthSchemas.Admin)
            .WithSummary("Remove the pet from tag (admin).")
            .WithDescription("Remove the pet from tag force with admin permissions.")
            .Produces<VoidResponseDto>();

        group.MapGet("tags/reports", GetAllTagReports)
            .RequireAuthorization(AuthSchemas.Admin)
            .WithSummary("Get all reports (admin).")
            .WithDescription("Get all reports with abused tags with filters(isResolved, tagId).")
            .Produces<TagReportsDto>();

        group.MapPost("tags/{id:int}/reports", CreateTagReport)
            .RequireAuthorization(AuthSchemas.Admin)
            .WithSummary("Create report for tag (admin).")
            .WithDescription("Create report that by opinion of admin was abused.")
            .Produces<VoidResponseDto>();

        group.MapPost("reports/{id:guid}/resolve", ResolveTagReport)
            .RequireAuthorization(AuthSchemas.Admin)
            .WithSummary("Mark report as resolved (admin).")
            .WithDescription(
                "To not pay attention for already resolved reports it must be marked as resolved.")
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
