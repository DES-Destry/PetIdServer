using Microsoft.EntityFrameworkCore;
using PetIdServer.Application.TagReports;
using PetIdServer.Application.TagReports.Dto;
using PetIdServer.Application.TagReports.Dto.Input;
using PetIdServer.Core.TagReports;
using PetIdServer.Core.Tags;
using PetIdServer.Persistence.Entities;
using PetIdServer.Persistence.Mapper;

namespace PetIdServer.Persistence.Repositories;

public class TagReportRepository(PetIdContext database) : ITagReportRepository
{
    public async Task<TagReportDto?> GetTagReportById(TagReportId id)
    {
        TagReportEntity? reportEntity = await database.TagReports.FirstOrDefaultAsync(report => report.Id == id);
        return reportEntity?.ToDto(reportEntity.CorruptedTag);
    }

    public async Task<IEnumerable<TagReportDto>> GetAllReports(GetReportsFilters filters)
    {
        List<TagReportEntity> reportEntities = await database.TagReports
            .Where(report => (filters.TagId == null || report.CorruptedTagId == filters.TagId) &&
                             (filters.IsResolved == null ||
                              (report.ResolverId != null || !filters.IsResolved.Value) &&
                              (report.ResolverId == null || filters.IsResolved.Value)))
            .OrderBy(report => report.ResolverId != null)
            .ToListAsync();

        return reportEntities.Select(report => report.ToDto(report.CorruptedTag));
    }

    public async Task<IEnumerable<TagReportDto>> GetReportsByTagId(TagId tagId)
    {
        List<TagReportEntity> reportEntities = await database.TagReports
            .Where(report => report.CorruptedTagId == tagId)
            .ToListAsync();

        return reportEntities.Select(report => report.ToDto(report.CorruptedTag));
    }
}
