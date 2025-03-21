using Carter;
using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.TagReports.Commands.Resolve;
using PetIdServer.Application.Tags.Commands.Clear;
using PetIdServer.Application.Tags.Commands.CreateBatch;
using PetIdServer.Application.Tags.Dto;
using PetIdServer.Application.Tags.Queries.GetAll;
using PetIdServer.Application.Tags.Queries.GetDecoded;
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

        group.MapPost("reports/{id:guid}/resolve", ResolveTagReport)
            .RequireAuthorization(AuthSchemas.Admin)
            .WithSummary("Mark report as resolved (admin).")
            .WithDescription(
                "To not pay attention for already resolved reports it must be marked as resolved.")
            .Produces<VoidResponseDto>();
    }

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

    private static async Task<IResult> CreateTags(CreateTagsDto dto, ISender sender)
    {
        CreateTagsBatchCommand command = dto.ToCommand();
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
