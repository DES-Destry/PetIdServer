using AutoMapper;
using MediatR;
using PetIdServer.Application.TagReports.Dto;
using PetIdServer.Application.TagReports.Dto.Input;
using PetIdServer.Core.TagReports;

namespace PetIdServer.Application.TagReports.Queries.GetAll;

public class GetAllTagReportsQueryHandler(IMapper mapper, ITagReportRepository reportRepository)
    : IRequestHandler<GetAllTagReportsQuery, TagReportsDto>
{
    public async Task<TagReportsDto> Handle(
        GetAllTagReportsQuery request,
        CancellationToken cancellationToken)
    {
        IEnumerable<TagReport>? reports =
            await reportRepository.GetAllReports(new GetReportsFilters(
                                                     request.TagId,
                                                     request.IsResolved));

        return new TagReportsDto(reports.Select(mapper.Map<TagReport, TagReportShortDto>));
    }
}
