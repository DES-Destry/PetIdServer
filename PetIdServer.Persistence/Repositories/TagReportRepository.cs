using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetIdServer.Application.TagReports;
using PetIdServer.Application.TagReports.Dto.Input;
using PetIdServer.Core.TagReports;
using PetIdServer.Core.Tags;
using PetIdServer.Persistence.Entities;

namespace PetIdServer.Infrastructure.Database.Repositories;

public class TagReportRepository(IMapper mapper, PetIdContext database) : ITagReportRepository
{
    public async Task<TagReport?> GetTagReportById(TagReportId id)
    {
        TagReportEntity? reportModel = await database.TagReports.FirstOrDefaultAsync(report => report.Id == id);
        return reportModel is not null
            ? mapper.Map<TagReportEntity, TagReport>(reportModel)
            : null;
    }

    public async Task<IEnumerable<TagReport>> GetAllReports(GetReportsFilters filters)
    {
        List<TagReportEntity> reportModels = await database.TagReports
            .Where(report => (filters.TagId == null || report.CorruptedTagId == filters.TagId) &&
                             (filters.IsResolved == null ||
                              (report.ResolverId != null || !filters.IsResolved.Value) &&
                              (report.ResolverId == null || filters.IsResolved.Value)))
            .OrderBy(report => report.ResolverId != null)
            .ToListAsync();

        return reportModels.Select(mapper.Map<TagReportEntity, TagReport>);
    }

    public async Task<IEnumerable<TagReport>> GetReportsByTagId(TagId tagId)
    {
        List<TagReportEntity> reportModels = await database.TagReports
            .Where(report => report.CorruptedTagId == tagId)
            .ToListAsync();

        return reportModels.Select(mapper.Map<TagReportEntity, TagReport>);
    }

    public async Task CreateReport(TagReport report)
    {
        TagReportEntity? model = mapper.Map<TagReport, TagReportEntity>(report);
        database.Entry(model).State = EntityState.Added;

        await database.SaveChangesAsync();
    }

    public async Task UpdateReport(TagReportId id, TagReport updated)
    {
        TagReportEntity? incomingData = mapper.Map<TagReport, TagReportEntity>(updated);
        TagReportEntity? model = await database.TagReports.FirstOrDefaultAsync(report => report.Id == id);

        if (model is null)
        {
            return;
        }

        database.Entry(model).CurrentValues.SetValues(incomingData);
        await database.SaveChangesAsync();
    }
}
