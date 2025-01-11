using PetIdServer.Core.Pets;

namespace PetIdServer.Application.Pets;

public interface IPetRepository
{
    Task<Pet?> GetPetById(PetId id);
    Task CreatePet(Pet pet);

    Task UpdatePet(PetId petId, Pet pet);
}
