using PetIdServer.Core.Domain.Pet;

namespace PetIdServer.Application.Domain.Pet;

public interface IPetRepository
{
    Task<Core.Domain.Pet.Pet?> GetPetById(PetId id);
    Task<Core.Domain.Pet.Pet?> CreatePet(Core.Domain.Pet.Pet pet);

    Task UpdatePet(PetId petId, Core.Domain.Pet.Pet pet);
}
