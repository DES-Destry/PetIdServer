using System.Diagnostics.CodeAnalysis;
using PetIdServer.Core.Tags;

namespace PetIdServer.Application.Pets.Dto;

public record CheckPetDto(string OwnerEmail, string Name)
{
    [return: NotNullIfNotNull("tag")]
    public static implicit operator CheckPetDto?(Tag? tag) =>
        tag is null ? null : new CheckPetDto(tag.Pet?.User?.Email ?? "Unclear", tag.Pet?.Name ?? "Unclear");
}
