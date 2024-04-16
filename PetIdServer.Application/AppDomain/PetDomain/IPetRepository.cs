using PetIdServer.Core.Domain.Pet;

namespace PetIdServer.Application.AppDomain.PetDomain;

public interface IPetRepository
{
    Task<PetEntity?> GetPetById(PetId id);
    Task CreatePet(PetEntity pet);

    Task UpdatePet(PetId petId, PetEntity pet);
}
