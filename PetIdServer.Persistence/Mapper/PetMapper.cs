using PetIdServer.Core.Pets;
using PetIdServer.Core.Users;
using PetIdServer.Persistence.Entities;

namespace PetIdServer.Persistence.Mapper;

public static class PetMapper
{
    public static Pet ToCore(this PetEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return Pet.CreateFromPersistence((PetId)entity.Id,
                                         (UserId)entity.UserId,
                                         entity.Name,
                                         entity.Type,
                                         entity.Sex,
                                         entity.IsCastrated,
                                         entity.PhotoId,
                                         entity.Description);
    }

    public static PetEntity ToEntity(this Pet pet)
    {
        ArgumentNullException.ThrowIfNull(pet);

        return new PetEntity
        {
            Id = pet.Id,
            UserId = (Guid)pet.OwnerId,
            Type = pet.Type,
            Name = pet.Name,
            Sex = pet.Sex,
            IsCastrated = pet.IsCastrated,
            PhotoId = pet.PhotoId,
            Description = pet.Description
        };
    }
}
