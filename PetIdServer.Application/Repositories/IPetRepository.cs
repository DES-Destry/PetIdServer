using PetIdServer.Core.Entities;

namespace PetIdServer.Application.Repositories;

public interface IPetRepository
{
    Task<Pet?> GetPetById(Guid id);
    Task<Pet?> CreatePet(Pet pet);
    
    Task UpdatePet(Guid petId, Pet pet);
}