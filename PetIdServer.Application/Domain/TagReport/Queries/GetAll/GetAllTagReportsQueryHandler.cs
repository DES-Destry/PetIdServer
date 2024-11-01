using AutoMapper;
using MediatR;
using PetIdServer.Application.Domain.TagReport.Dto;
using PetIdServer.Application.Domain.TagReport.Dto.Input;
using PetIdServer.Core.Domain.TagReport;

namespace PetIdServer.Application.Domain.TagReport.Queries.GetAll;

public class GetAllTagReportsQueryHandler(IMapper mapper, ITagReportRepository reportRepository)
    : IRequestHandler<GetAllTagReportsQuery, TagReportsDto>
{
    public async Task<TagReportsDto> Handle(
        GetAllTagReportsQuery request,
        CancellationToken cancellationToken)
    {
        var reports =
            await reportRepository.GetAllReports(new GetReportsFilters(
                request.TagId,
                request.IsResolved));

        return new TagReportsDto(reports.Select(mapper.Map<TagReportEntity, TagReportShortDto>));
    }
}
