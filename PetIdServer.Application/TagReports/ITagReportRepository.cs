using PetIdServer.Application.TagReports.Dto;
using PetIdServer.Application.TagReports.Dto.Input;
using PetIdServer.Core.TagReports;
using PetIdServer.Core.Tags;

namespace PetIdServer.Application.TagReports;

public interface ITagReportRepository
{
    Task<TagReportDto?> GetTagReportById(TagReportId id);
    Task<IEnumerable<TagReportDto>> GetAllReports(GetReportsFilters filters);
    Task<IEnumerable<TagReportDto>> GetReportsByTagId(TagId tagId);
}
