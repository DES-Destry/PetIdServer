using PetIdServer.Core.TagReports;
using PetIdServer.Core.Tags;
using PetIdServer.Core.Users;

namespace PetIdServer.Application.TagReports.Dto;

public record TagReportDto(
    TagReportId Id,
    TagId CorruptedTagId,
    UserId ReporterId,
    UserId? ResolverId,
    DateTime CreatedAt,
    DateTime? ResolvedAt);
