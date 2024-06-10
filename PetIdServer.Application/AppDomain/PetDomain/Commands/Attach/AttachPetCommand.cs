using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.AppDomain.PetDomain.Commands.Attach;

public class AttachPetCommand : IRequest<VoidResponseDto>
{
    public Guid PetId { get; set; }
    public string TagCode { get; set; }
}
