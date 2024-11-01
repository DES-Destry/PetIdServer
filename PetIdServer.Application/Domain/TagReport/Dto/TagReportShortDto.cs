using PetIdServer.Application.Common.Services.Dto;
using PetIdServer.Application.Domain.Tag.Dto;

namespace PetIdServer.Application.Domain.TagReport.Dto;

public record TagReportShortDto(
    Guid Id,
    TagReviewForAdminDto CorruptedTag,
    AdminDto Reporter,
    AdminDto? Resolver,
    bool IsResolved,
    DateTime CreatedAt);
