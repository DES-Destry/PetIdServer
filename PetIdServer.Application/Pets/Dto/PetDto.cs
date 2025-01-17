using System.Diagnostics.CodeAnalysis;
using PetIdServer.Core.Pets;

namespace PetIdServer.Application.Pets.Dto;

public record PetDto(string Type, string Name, bool Sex, bool IsCastrated, Guid? PhotoId, string? Description)
{
    [return: NotNullIfNotNull("pet")]
    public static implicit operator PetDto?(Pet? pet) => pet is null
        ? null
        : new PetDto(pet.Type, pet.Name, pet.Sex, pet.IsCastrated, pet.PhotoId, pet.Description);
}
