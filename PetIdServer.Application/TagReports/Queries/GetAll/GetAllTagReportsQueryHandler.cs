using MediatR;
using PetIdServer.Application.TagReports.Dto;
using PetIdServer.Application.TagReports.Dto.Input;
using PetIdServer.Application.Tags;
using PetIdServer.Application.Users;
using PetIdServer.Core.Tags;
using PetIdServer.Core.Users;

namespace PetIdServer.Application.TagReports.Queries.GetAll;

public class GetAllTagReportsQueryHandler(
    ITagReportRepository reportRepository,
    ITagRepository tagRepository,
    IUserRepository userRepository)
    : IRequestHandler<GetAllTagReportsQuery, TagReportsDto>
{
    public async Task<TagReportsDto> Handle(
        GetAllTagReportsQuery request,
        CancellationToken cancellationToken)
    {
        GetReportsFilters filters = new(request.TagId, request.IsResolved);
        IEnumerable<TagReportDto> reports = await reportRepository.GetAllReports(filters);

        TagReportDetailedDto[] detailedReports = await GetDetailedReports(reports);
        return new TagReportsDto(detailedReports);
    }

    private async Task<TagReportDetailedDto[]> GetDetailedReports(IEnumerable<TagReportDto> reports)
    {
        return await Task.WhenAll(reports.Select(GetDetailsAboutTagReport));
    }

    private async Task<TagReportDetailedDto> GetDetailsAboutTagReport(TagReportDto report)
    {
        Task<Tag?> corruptedTagTask = tagRepository.GetTagById(report.CorruptedTagId);

        Task<User?> reporterTask = userRepository.GetUserById(report.ReporterId);
        Task<User?> resolverTask = report.ResolverId != null
            ? userRepository.GetUserById(report.ResolverId)
            : Task.FromResult<User?>(null);

        await Task.WhenAll(corruptedTagTask, reporterTask, resolverTask);

        return new TagReportDetailedDto(
            report.Id,
            await corruptedTagTask,
            await reporterTask,
            await resolverTask,
            report.ResolvedAt != null,
            report.CreatedAt);
    }
}
