using AutoMapper;
using MediatR;
using PetIdServer.Application.TagReport.Dto;
using PetIdServer.Application.TagReport.Dto.Input;
using PetIdServer.Core.Domain.TagReport;

namespace PetIdServer.Application.TagReport.Queries.GetAll;

public class GetAllTagReportsQueryHandler(IMapper mapper, ITagReportRepository reportRepository)
    : IRequestHandler<GetAllTagReportsQuery, TagReportsDto>
{
    public async Task<TagReportsDto> Handle(
        GetAllTagReportsQuery request,
        CancellationToken cancellationToken)
    {
        IEnumerable<TagReportEntity>? reports =
            await reportRepository.GetAllReports(new GetReportsFilters(
                                                     request.TagId,
                                                     request.IsResolved));

        return new TagReportsDto(reports.Select(mapper.Map<TagReportEntity, TagReportShortDto>));
    }
}
