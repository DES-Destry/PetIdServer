using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.Domain.TagReport.Commands.Resolve;

public class ResolveTagReportCommand : IRequest<VoidResponseDto>
{
    public Guid AdminId { get; set; }
    public Guid ReportId { get; set; }
}
