using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.AppDomain.TagDomain.Commands.Clear;

public class ClearTagCommand : IRequest<VoidResponseDto>
{
    public string AdminId { get; set; }
    public int TagId { get; set; }
}
