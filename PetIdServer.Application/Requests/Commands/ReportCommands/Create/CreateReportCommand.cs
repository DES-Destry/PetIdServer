using MediatR;
using PetIdServer.Application.Dto;
using PetIdServer.Core.Domain.Admin;
using PetIdServer.Core.Domain.Tag;

namespace PetIdServer.Application.Requests.Commands.ReportCommands.Create;

public class CreateReportCommand : IRequest<VoidResponseDto>
{
    public AdminId AdminId { get; set; }
    public TagId TagId { get; set; }
}
