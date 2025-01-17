using Microsoft.EntityFrameworkCore;
using PetIdServer.Application.TagReports;
using PetIdServer.Application.TagReports.Dto.Input;
using PetIdServer.Core.TagReports;
using PetIdServer.Core.Tags;
using PetIdServer.Persistence.Entities;
using PetIdServer.Persistence.Mapper;

namespace PetIdServer.Persistence.Repositories;

public class TagReportRepository(PetIdContext database) : ITagReportRepository
{
    public async Task<TagReport?> GetTagReportById(TagReportId id)
    {
        TagReportEntity? reportEntity = await database.TagReports.FirstOrDefaultAsync(report => report.Id == id);
        return reportEntity?.ToCore();
    }

    public async Task<IEnumerable<TagReport>> GetAllReports(GetReportsFilters filters)
    {
        List<TagReportEntity> reportEnteties = await database.TagReports
            .Where(report => (filters.TagId == null || report.CorruptedTagId == filters.TagId) &&
                             (filters.IsResolved == null ||
                              (report.ResolverId != null || !filters.IsResolved.Value) &&
                              (report.ResolverId == null || filters.IsResolved.Value)))
            .OrderBy(report => report.ResolverId != null)
            .ToListAsync();

        return reportEnteties.Select(TagReportMapper.ToCore);
    }

    public async Task<IEnumerable<TagReport>> GetReportsByTagId(TagId tagId)
    {
        List<TagReportEntity> reportEntities = await database.TagReports
            .Where(report => report.CorruptedTagId == tagId)
            .ToListAsync();

        return reportEntities.Select(TagReportMapper.ToCore);
    }

    public async Task CreateReport(TagReport report, Tag tag)
    {
        TagReportEntity entity = report.ToEntity(tag);
        database.Entry(entity).State = EntityState.Added;

        await database.SaveChangesAsync();
    }

    public async Task UpdateReport(TagReport updated, Tag tag)
    {
        TagReportEntity incomingData = updated.ToEntity(tag);
        TagReportEntity? reportEntity = await database.TagReports.FirstOrDefaultAsync(report => report.Id == updated.Id);

        if (reportEntity is null)
        {
            return;
        }

        database.Entry(reportEntity).CurrentValues.SetValues(incomingData);
        await database.SaveChangesAsync();
    }
}
