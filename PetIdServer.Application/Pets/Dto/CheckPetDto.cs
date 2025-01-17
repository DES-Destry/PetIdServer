using PetIdServer.Core.Pets;
using PetIdServer.Core.Users;

namespace PetIdServer.Application.Pets.Dto;

public record CheckPetDto(string OwnerEmail, string Name)
{
    public static CheckPetDto FromPetAndHisOwner(Pet? pet, User? owner) => new(owner?.Email ?? "Unclear", pet?.Name ?? "Unclear");
}
