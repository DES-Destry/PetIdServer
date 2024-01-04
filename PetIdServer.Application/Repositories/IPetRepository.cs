using PetIdServer.Core.Domain.Pet;

namespace PetIdServer.Application.Repositories;

public interface IPetRepository
{
    Task<Pet?> GetPetById(PetId id);
    Task<Pet?> CreatePet(Pet pet);

    Task UpdatePet(PetId petId, Pet pet);
}
