using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.Domain.TagReport.Commands.Create;

public class CreateTagReportCommand : IRequest<VoidResponseDto>
{
    public Guid AdminId { get; set; }
    public int TagId { get; set; }
}
