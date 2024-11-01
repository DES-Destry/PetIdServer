using PetIdServer.Application.Domain.TagReport.Dto.Input;
using PetIdServer.Core.Domain.Tag;
using PetIdServer.Core.Domain.TagReport;

namespace PetIdServer.Application.Domain.TagReport;

public interface ITagReportRepository
{
    Task<TagReportEntity?> GetTagReportById(TagReportId id);
    Task<IEnumerable<TagReportEntity>> GetAllReports(GetReportsFilters filters);
    Task<IEnumerable<TagReportEntity>> GetReportsByTagId(TagId tagId);

    Task CreateReport(TagReportEntity report);
    Task UpdateReport(TagReportId id, TagReportEntity updated);
}
