using PetIdServer.Application.Tag.Dto;
using PetIdServer.Application.User.Dto;

namespace PetIdServer.Application.TagReport.Dto;

public record TagReportShortDto(
    Guid Id,
    TagReviewForAdminDto CorruptedTag,
    UserDto Reporter,
    UserDto? Resolver,
    bool IsResolved,
    DateTime CreatedAt);
