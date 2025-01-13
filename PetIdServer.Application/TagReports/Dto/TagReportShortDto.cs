using System.Diagnostics.CodeAnalysis;
using PetIdServer.Application.Tags.Dto;
using PetIdServer.Application.Users.Dto;
using PetIdServer.Core.TagReports;

namespace PetIdServer.Application.TagReports.Dto;

public record TagReportShortDto(
    Guid Id,
    TagReviewForAdminDto CorruptedTag,
    UserDto Reporter,
    UserDto? Resolver,
    bool IsResolved,
    DateTime CreatedAt)
{
    [return: NotNullIfNotNull("report")]
    public static implicit operator TagReportShortDto?(TagReport? report) => report is null
        ? null
        : new TagReportShortDto(
            report.Id, report.CorruptedTag, report.Reporter, report.Resolver,
            report.IsResolved, report.CreatedAt);
}
