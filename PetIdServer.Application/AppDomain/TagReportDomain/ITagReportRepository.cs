using PetIdServer.Core.Domain.Tag;
using PetIdServer.Core.Domain.TagReport;

namespace PetIdServer.Application.AppDomain.TagReportDomain;

public interface ITagReportRepository
{
    Task<IEnumerable<TagReport>> GetAllReports();
    Task<IEnumerable<TagReport>> GetNotResolvedReports();
    Task<IEnumerable<TagReport>> GetReportsByTagId(TagId tagId);

    Task CreateReport(TagReport report);
    Task UpdateReport(TagReportId id, TagReport updated);
}
