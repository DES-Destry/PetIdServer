using PetIdServer.Core.Entities;

namespace PetIdServer.Application.Repositories;

public interface IOwnerRepository
{
    Task<Owner?> GetOwnerById(Guid id);
    Task<Owner?> GetOwnerByEmail(string email);
    Task<Owner?> CreateOwner(Owner owner);

    Task UpdateOwner(Guid ownerId, Owner owner);
}