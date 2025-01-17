using PetIdServer.Application.TagReports.Dto.Input;
using PetIdServer.Core.TagReports;
using PetIdServer.Core.Tags;

namespace PetIdServer.Application.TagReports;

public interface ITagReportRepository
{
    Task<TagReport?> GetTagReportById(TagReportId id);
    Task<IEnumerable<TagReport>> GetAllReports(GetReportsFilters filters);
    Task<IEnumerable<TagReport>> GetReportsByTagId(TagId tagId);

    Task CreateReport(TagReport report, Tag tag);
    Task UpdateReport(TagReport updated, Tag tag);
}
