using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.AppDomain.TagReportDomain.Commands.Resolve;

public class ResolveTagReportCommand : IRequest<VoidResponseDto>
{
    public string AdminId { get; set; }
    public Guid ReportId { get; set; }
}
