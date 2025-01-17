using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.TagReports.Commands.Resolve;

public class ResolveTagReportCommand : IRequest<VoidResponseDto>
{
    public required Guid AdminId { get; init; }
    public required Guid ReportId { get; init; }
}
