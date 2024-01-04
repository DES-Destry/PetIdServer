using PetIdServer.Core.Domain.Owner;

namespace PetIdServer.Application.Repositories;

public interface IOwnerRepository
{
    Task<Owner?> GetOwnerById(OwnerId id);
    Task<Owner?> GetOwnerByEmail(string email);
    Task<Owner?> CreateOwner(Owner owner);

    Task UpdateOwner(OwnerId id, Owner owner);
}
