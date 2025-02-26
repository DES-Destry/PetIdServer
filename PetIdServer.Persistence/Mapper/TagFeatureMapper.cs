using PetIdServer.Core.Tags;
using PetIdServer.Persistence.Entities;

namespace PetIdServer.Persistence.Mapper;

public static class TagFeatureMapper
{
    public static TagFeature ToCore(this TagFeatureEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        return new TagFeature(entity.Feature);
    }

    public static TagFeatureEntity ToEntity(this TagFeature feature, Tag tag)
    {
        ArgumentNullException.ThrowIfNull(feature);

        return new TagFeatureEntity
        {
            TagId = tag.Id, Feature = feature.Value
        };
    }
}
