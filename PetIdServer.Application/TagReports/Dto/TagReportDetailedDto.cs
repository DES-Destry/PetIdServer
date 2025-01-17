using PetIdServer.Application.Tags.Dto;
using PetIdServer.Application.Users.Dto;

namespace PetIdServer.Application.TagReports.Dto;

public record TagReportDetailedDto(
    Guid Id,
    TagReviewForAdminDto? CorruptedTag,
    UserDto? Reporter,
    UserDto? Resolver,
    bool IsResolved,
    DateTime CreatedAt);
