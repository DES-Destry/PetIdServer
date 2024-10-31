using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetIdServer.Application.AppDomain.TagReportDomain;
using PetIdServer.Application.AppDomain.TagReportDomain.Dto.Input;
using PetIdServer.Core.Domain.Tag;
using PetIdServer.Core.Domain.TagReport;

namespace PetIdServer.Infrastructure.Database.Domain.Tag;

public class TagReportRepository(IMapper mapper, PetIdContext database) : ITagReportRepository
{
    public async Task<TagReportEntity?> GetTagReportById(TagReportId id)
    {
        TagReportModel? reportModel = await database.TagReports.FirstOrDefaultAsync(report => report.Id == id);
        return reportModel is not null
            ? mapper.Map<TagReportModel, TagReportEntity>(reportModel)
            : null;
    }

    public async Task<IEnumerable<TagReportEntity>> GetAllReports(GetReportsFilters filters)
    {
        List<TagReportModel> reportModels = await database.TagReports
            .Where(report => (filters.TagId == null || report.CorruptedTagId == filters.TagId) &&
                             (filters.IsResolved == null ||
                              (report.ResolverId != null || !filters.IsResolved.Value) &&
                              (report.ResolverId == null || filters.IsResolved.Value)))
            .OrderBy(report => report.ResolverId != null)
            .ToListAsync();

        return reportModels.Select(mapper.Map<TagReportModel, TagReportEntity>);
    }

    public async Task<IEnumerable<TagReportEntity>> GetReportsByTagId(TagId tagId)
    {
        List<TagReportModel> reportModels = await database.TagReports
            .Where(report => report.CorruptedTagId == tagId)
            .ToListAsync();

        return reportModels.Select(mapper.Map<TagReportModel, TagReportEntity>);
    }

    public async Task CreateReport(TagReportEntity report)
    {
        TagReportModel? model = mapper.Map<TagReportEntity, TagReportModel>(report);
        database.Entry(model).State = EntityState.Added;

        await database.SaveChangesAsync();
    }

    public async Task UpdateReport(TagReportId id, TagReportEntity updated)
    {
        TagReportModel? incomingData = mapper.Map<TagReportEntity, TagReportModel>(updated);
        TagReportModel? model = await database.TagReports.FirstOrDefaultAsync(report => report.Id == id);

        if (model is null)
        {
            return;
        }

        database.Entry(model).CurrentValues.SetValues(incomingData);
        await database.SaveChangesAsync();
    }
}
