using PetIdServer.Core.Domain.Pet;

namespace PetIdServer.Application.Domain.Pet;

public interface IPetRepository
{
    Task<PetEntity?> GetPetById(PetId id);
    Task CreatePet(PetEntity pet);

    Task UpdatePet(PetId petId, PetEntity pet);
}
