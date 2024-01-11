using AutoMapper;
using MediatR;
using PetIdServer.Application.AppDomain.TagReportDomain.Dto;
using PetIdServer.Application.AppDomain.TagReportDomain.Dto.Input;
using PetIdServer.Core.Domain.TagReport;

namespace PetIdServer.Application.AppDomain.TagReportDomain.Queries.GetAll;

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
