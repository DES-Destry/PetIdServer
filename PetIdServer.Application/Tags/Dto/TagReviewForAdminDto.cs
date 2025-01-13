using System.Diagnostics.CodeAnalysis;
using PetIdServer.Core.Tags;

namespace PetIdServer.Application.Tags.Dto;

public record TagReviewForAdminDto(int Id, bool IsAlreadyInUse, DateTime CreatedAt)
{
    [return: NotNullIfNotNull("tag")]
    public static implicit operator TagReviewForAdminDto?(Tag? tag) =>
        tag is null ? null : new TagReviewForAdminDto(tag.Id, tag.IsAlreadyInUse, tag.CreatedAt);
}
