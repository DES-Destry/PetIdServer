using PetIdServer.Application.TagReport.Dto.Input;
using PetIdServer.Core.Domain.Tag;
using PetIdServer.Core.Domain.TagReport;

namespace PetIdServer.Application.TagReport;

public interface ITagReportRepository
{
    Task<TagReportEntity?> GetTagReportById(TagReportId id);
    Task<IEnumerable<TagReportEntity>> GetAllReports(GetReportsFilters filters);
    Task<IEnumerable<TagReportEntity>> GetReportsByTagId(TagId tagId);

    Task CreateReport(TagReportEntity report);
    Task UpdateReport(TagReportId id, TagReportEntity updated);
}
