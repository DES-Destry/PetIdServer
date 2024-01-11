using PetIdServer.Core.Domain.Owner;

namespace PetIdServer.Application.AppDomain.OwnerDomain;

public interface IOwnerRepository
{
    Task<OwnerEntity?> GetOwnerById(OwnerId id);
    Task<OwnerEntity?> GetOwnerByEmail(string email);
    Task<OwnerEntity?> CreateOwner(OwnerEntity owner);

    Task UpdateOwner(OwnerId id, OwnerEntity owner);
}
