using PetIdServer.Core.Pets;
using PetIdServer.Core.TagReports;
using PetIdServer.Core.Tags;
using PetIdServer.Persistence.Entities;

namespace PetIdServer.Persistence.Mapper;

public static class TagMapper
{
    public static Tag ToCore(this TagEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        IEnumerable<TagReport> reports = entity.Reports.Select(TagReportMapper.ToCore);
        IEnumerable<TagFeature> features = entity.Features.Select(TagFeatureMapper.ToCore);
        IEnumerable<TagHistoryEntry> history = entity.History.Select(TagHistoryEntryMapper.ToCore);

        return Tag.CreateFromPersistence(
            (TagId)entity.Id,
            entity.Code,
            entity.HashCode,
            entity.ControlCode,
            (PetId?)entity.PetId,
            reports,
            features,
            history,
            entity.CreatedAt,
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
            Reports = tag.Reports.Select(report => report.ToEntity(tag)).ToList(),
            Features = tag.Features.Select(feature => feature.ToEntity(tag)).ToList(),
            History = tag.History.Select(entry => entry.ToEntity(tag)).ToList(),
            CreatedAt = tag.CreatedAt,
            LastScannedAt = tag.LastScannedAt
        };
    }
}
