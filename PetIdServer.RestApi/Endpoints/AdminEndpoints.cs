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
    private const string EndpointBase = "api/admin";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(EndpointBase).RequireSecurityKey().WithOpenApi();
        var authorizedGroup = group.RequireAuthorization(AuthSchemas.Admin);

        authorizedGroup.MapGet("auth", Authenticate)
            .WithSummary("Get information about admin from token (admin).")
            .Produces<AdminDto>();

        group.MapPost("login", LoginAdmin)
            .WithSummary("Login existed administrator in system (admin).")
            .Produces<LoginAdminResponseDto>();

        authorizedGroup.MapPut("password", ChangePassword)
            .WithSummary("Change password as authenticated admin.")
            .Produces<SingleTokenDto>();

        authorizedGroup.MapGet("tags", GetAllTags)
            .WithSummary("Get all tags (admin).")
            .WithDescription("Get all tags with shorted amount of fields.")
            .Produces<TagReviewList>();

        authorizedGroup.MapGet("tags/{id:int}", GetDecodedTag)
            .WithSummary("Get decoded tag (admin).")
            .WithDescription("Get tag with his public code with admin credentials.")
            .Produces<TagForAdminDto>();

        authorizedGroup.MapPost("tags", CreateTags)
            .WithSummary("Create tags range (admin).")
            .WithDescription(
                "Describe range and it will create all tags from x to y. Be careful with conflicts!")
            .Produces<VoidResponseDto>();

        authorizedGroup.MapPost("tags/{id:int}/clear", ClearTag)
            .WithSummary("Remove the pet from tag (admin).")
            .WithDescription("Remove the pet from tag force with admin permissions.")
            .Produces<VoidResponseDto>();

        authorizedGroup.MapGet("tags/reports", GetAllTagReports)
            .WithSummary("Get all reports (admin).")
            .WithDescription("Get all reports with abused tags with filters(isResolved, tagId).")
            .Produces<TagReportsDto>();

        authorizedGroup.MapPost("tags/{id:int}/reports", CreateTagReport)
            .WithSummary("Create report for tag (admin).")
            .WithDescription("Create report that by opinion of admin was abused.")
            .Produces<VoidResponseDto>();

        authorizedGroup.MapPost("reports/{id:guid}/resolve", ResolveTagReport)
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
