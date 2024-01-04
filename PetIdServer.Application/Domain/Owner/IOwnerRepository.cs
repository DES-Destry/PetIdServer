using PetIdServer.Core.Domain.Owner;

namespace PetIdServer.Application.Domain.Owner;

public interface IOwnerRepository
{
    Task<Core.Domain.Owner.Owner?> GetOwnerById(OwnerId id);
    Task<Core.Domain.Owner.Owner?> GetOwnerByEmail(string email);
    Task<Core.Domain.Owner.Owner?> CreateOwner(Core.Domain.Owner.Owner owner);

    Task UpdateOwner(OwnerId id, Core.Domain.Owner.Owner owner);
}
