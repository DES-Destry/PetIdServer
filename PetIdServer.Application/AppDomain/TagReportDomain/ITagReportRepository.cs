using PetIdServer.Core.Domain.Tag;
using PetIdServer.Core.Domain.TagReport;

namespace PetIdServer.Application.AppDomain.TagReportDomain;

public interface ITagReportRepository
{
    Task<TagReportEntity?> GetTagReportById(TagReportId id);
    Task<IEnumerable<TagReportEntity>> GetAllReports();
    Task<IEnumerable<TagReportEntity>> GetNotResolvedReports();
    Task<IEnumerable<TagReportEntity>> GetReportsByTagId(TagId tagId);

    Task CreateReport(TagReportEntity report);
    Task UpdateReport(TagReportId id, TagReportEntity updated);
}
