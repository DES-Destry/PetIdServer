using PetIdServer.Core.Pets;
using PetIdServer.Core.Tags;
using PetIdServer.Persistence.Entities;

namespace PetIdServer.Persistence.Mapper;

public static class TagMapper
{
    public static Tag ToCore(this TagEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        IEnumerable<TagFeature> features = entity.Features.Select(f => new TagFeature(f.Feature));

        return Tag.CreateFromPersistence(
            (TagId)entity.Id,
            entity.Code,
            entity.HashCode,
            entity.ControlCode,
            (PetId?)entity.PetId,
            features,
            entity.CreatedAt,
            entity.PetAddedAt,
            entity.LastScannedAt
        );
    }

    public static TagEntity ToEntity(this Tag tag)
    {
        ArgumentNullException.ThrowIfNull(tag);

        return new TagEntity
        {
            Id = tag.Id,
            Code = tag.PrivateCode,
            HashCode = tag.HashCode,
            ControlCode = tag.ControlCode,
            PetId = tag.PetId,
            Features =
                tag.Features.Select(f => new TagFeatureEntity
                {
                    TagId = tag.Id, Feature = f.Value
                }).ToList(),
            CreatedAt = tag.CreatedAt,
            LastScannedAt = tag.LastScannedAt
        };
    }
}
