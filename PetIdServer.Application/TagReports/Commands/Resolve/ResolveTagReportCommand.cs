using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.TagReports.Commands.Resolve;

public class ResolveTagReportCommand : IRequest<VoidResponseDto>
{
    public Guid AdminId { get; set; }
    public Guid ReportId { get; set; }
}
