using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.TagReports.Commands.Create;
using PetIdServer.Application.TagReports.Dto;
using PetIdServer.Application.TagReports.Queries.GetAll;
using PetIdServer.Application.Tags.Dto;
using PetIdServer.Application.Tags.Queries.ControlCheck;
using PetIdServer.RestApi.Auth;
using PetIdServer.RestApi.Binding;

namespace PetIdServer.RestApi.Endpoints;

public class TagCheckerEndpoints : ICarterModule
{
    private const string EndpointBase = "api/tags";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup(EndpointBase).WithOpenApi();

        group.MapGet("pre-sell-check/{controlCode:long}", TagControlCheck)
            .WithOpenApi()
            .WithSummary("Get pre-sell info by control code.")
            .RequireAuthorization(AuthSchemas.TagChecker)
            .Produces<CheckTagDto>();

        group.MapGet("reports", GetAllTagReports)
            .RequireAuthorization(AuthSchemas.TagChecker)
            .WithSummary("Get all reports (tag-checker).")
            .WithDescription("Get all reports with abused tags with filters(isResolved, tagId).")
            .Produces<TagReportsDto>();

        group.MapPost("{id:int}/reports", CreateTagReport)
            .RequireAuthorization(AuthSchemas.TagChecker)
            .WithSummary("Create report for tag (tag-checker).")
            .WithDescription("Create report that by opinion of admin was abused.")
            .Produces<VoidResponseDto>();
    }

    private static async Task<IResult> TagControlCheck(long controlCode, ISender sender)
    {
        TagControlCheckQuery query = new()
        {
            ControlCode = controlCode
        };
        CheckTagDto response = await sender.Send(query);

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
}
