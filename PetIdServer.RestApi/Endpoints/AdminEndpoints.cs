using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Domain.Tag.Commands.Clear;
using PetIdServer.Application.Domain.Tag.Commands.CreateBatch;
using PetIdServer.Application.Domain.Tag.Dto;
using PetIdServer.Application.Domain.Tag.Queries.GetAll;
using PetIdServer.Application.Domain.Tag.Queries.GetDecoded;
using PetIdServer.Application.Domain.TagReport.Commands.Create;
using PetIdServer.Application.Domain.TagReport.Commands.Resolve;
using PetIdServer.Application.Domain.TagReport.Dto;
using PetIdServer.Application.Domain.TagReport.Queries.GetAll;
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
        RouteGroupBuilder group = app.MapGroup(EndpointBase).RequireSecurityKey().WithOpenApi();
        // var authorizedGroup = group.RequireAuthorization(AuthSchemas.Admin);

        // group.MapPut("password", ChangePassword)
        //     .RequireAuthorization(AuthSchemas.Admin)
        //     .WithSummary("Change password as authenticated admin.")
        //     .Produces<SingleTokenDto>();

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

    // TODO: Move to user controller
    // private static async Task<IResult> ChangePassword(
    //     RequestUser admin,
    //     ChangePasswordDto dto,
    //     ISender sender)
    // {
    //     ChangePasswordCommand command = new()
    //     {
    //         Id = admin.Email, OldPassword = dto.OldPassword, NewPassword = dto.NewPassword
    //     };
    //     SingleTokenDto response = await sender.Send(command);
    //
    //     return Results.Ok(response);
    // }

    private static async Task<IResult> GetAllTags(ISender sender)
    {
        GetAllTagsQuery query = new();
        TagReviewList response = await sender.Send(query);

        return Results.Ok(response);
    }

    private static async Task<IResult> GetDecodedTag(int id, ISender sender)
    {
        GetDecodedTagQuery query = new()
        {
            Id = id
        };
        TagForAdminDto response = await sender.Send(query);

        return Results.Ok(response);
    }

    private static async Task<IResult> CreateTags(CreateTagsDto dto, ISender sender, IMapper mapper)
    {
        CreateTagsBatchCommand? command = mapper.Map<CreateTagsDto, CreateTagsBatchCommand>(dto);
        VoidResponseDto response = await sender.Send(command);

        return Results.Ok(response);
    }

    private static async Task<IResult> ClearTag(int id, RequestUser admin, ISender sender)
    {
        ClearTagCommand command = new()
        {
            AdminId = admin.Id, TagId = id
        };
        VoidResponseDto response = await sender.Send(command);

        return Results.Ok(response);
    }

    private static async Task<IResult> GetAllTagReports(
        [FromQuery] int? tagId,
        [FromQuery] bool? isResolved,
        ISender sender)
    {
        GetAllTagReportsQuery query = new()
        {
            TagId = tagId, IsResolved = isResolved
        };
        TagReportsDto response = await sender.Send(query);

        return Results.Ok(response);
    }

    private static async Task<IResult> CreateTagReport(int id, RequestUser admin, ISender sender)
    {
        CreateTagReportCommand command = new()
        {
            AdminId = admin.Id, TagId = id
        };
        VoidResponseDto response = await sender.Send(command);

        return Results.Ok(response);
    }

    private static async Task<IResult> ResolveTagReport(Guid id, RequestUser admin, ISender sender)
    {
        ResolveTagReportCommand command = new()
        {
            AdminId = admin.Id, ReportId = id
        };
        VoidResponseDto response = await sender.Send(command);

        return Results.Ok(response);
    }
}
