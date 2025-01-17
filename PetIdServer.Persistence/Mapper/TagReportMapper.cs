using PetIdServer.Core.TagReports;
using PetIdServer.Core.Tags;
using PetIdServer.Core.Users;
using PetIdServer.Persistence.Entities;

namespace PetIdServer.Persistence.Mapper;

public static class TagReportMapper
{
    public static TagReport ToCore(this TagReportEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return TagReport.CreateFromPersistence(
            (TagReportId)entity.Id,
            (UserId)entity.ReporterId,
            (UserId?)entity.ResolverId,
            entity.CreatedAt,
            entity.ResolvedAt
        );
    }

    public static TagReportEntity ToEntity(this TagReport report, Tag reportedTag)
    {
        ArgumentNullException.ThrowIfNull(report);

        return new TagReportEntity
        {
            Id = report.Id,
            CorruptedTagId = reportedTag.Id,
            ReporterId = (Guid)report.ReporterId,
            ResolverId = report.ResolverId,
            CreatedAt = report.CreatedAt,
            ResolvedAt = report.ResolvedAt
        };
    }
}
