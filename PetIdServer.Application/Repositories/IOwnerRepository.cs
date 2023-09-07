using PetIdServer.Core.Entities;

namespace PetIdServer.Application.Repositories;

public interface IOwnerRepository
{
    Task<Owner?> GetOwnerByEmail(string email);
    Task<Owner?> CreateOwner(Owner owner);

    Task UpdateOwner(string ownerEmail, Owner owner);
}