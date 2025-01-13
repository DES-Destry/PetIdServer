using System.Diagnostics.CodeAnalysis;
using PetIdServer.Application.Pets.Dto;
using PetIdServer.Core.Tags;

namespace PetIdServer.Application.Tags.Dto;

public record TagDto(int Id, string Code, PetDto? Pet, bool IsAlreadyInUse, DateTime CreatedAt)
{
    [return: NotNullIfNotNull("tag")]
    public static implicit operator TagDto?(Tag? tag) => tag is null
        ? null
        : new TagDto(tag.Id, tag.PrivateCode, tag.Pet, tag.IsAlreadyInUse, tag.CreatedAt);
}
