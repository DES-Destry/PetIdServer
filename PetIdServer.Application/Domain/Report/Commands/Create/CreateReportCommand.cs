using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Core.Domain.Admin;
using PetIdServer.Core.Domain.Tag;

namespace PetIdServer.Application.Domain.Report.Commands.Create;

public class CreateReportCommand : IRequest<VoidResponseDto>
{
    public AdminId AdminId { get; set; }
    public TagId TagId { get; set; }
}
