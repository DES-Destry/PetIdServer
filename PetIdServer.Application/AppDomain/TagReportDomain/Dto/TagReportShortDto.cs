using PetIdServer.Application.AppDomain.TagDomain.Dto;
using PetIdServer.Application.Common.Services.Dto;

namespace PetIdServer.Application.AppDomain.TagReportDomain.Dto;

public record TagReportShortDto(
    Guid Id,
    TagReviewForAdminDto Tag,
    AdminDto Reporter,
    bool IsResolved,
    DateTime CreatedAt);
