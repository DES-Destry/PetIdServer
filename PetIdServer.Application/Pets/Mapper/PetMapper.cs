using PetIdServer.Application.Pets.Commands.Update;
using PetIdServer.Core.Pets;

namespace PetIdServer.Application.Pets.Mapper;

public static class PetMapper
{
    public static Pet MapUpdate(this Pet pet, UpdatePetCommand dto)
    {
        pet.Update(new Pet.UpdateAttributes
        {
            Description = dto.Description,
            IsCastrated = dto.IsCastrated,
            CanGoOutside = dto.CanGoOutside,
            Name = dto.Name,
            Sex = dto.Sex,
            Type = dto.Type
        });

        return pet;
    }
}
