using MediatR;
using PetIdServer.Application.Dto;
using PetIdServer.Core.Domains.Admin;
using PetIdServer.Core.Domains.Tag;

namespace PetIdServer.Application.Requests.Commands.Report.Create;

public class CreateReportCommand : IRequest<VoidResponseDto>
{
    public AdminId AdminId { get; set; }
    public TagId TagId { get; set; }
}
