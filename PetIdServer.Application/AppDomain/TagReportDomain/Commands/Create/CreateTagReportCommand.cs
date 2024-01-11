using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.AppDomain.TagReportDomain.Commands.Create;

public class CreateTagReportCommand : IRequest<VoidResponseDto>
{
    public string AdminId { get; set; }
    public int TagId { get; set; }
}
