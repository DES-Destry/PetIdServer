using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.AppDomain.OwnerDomain.Commands.RemoveContact;

public class RemoveContactCommand : IRequest<VoidResponseDto>
{
    public string OwnerEmail { get; set; }
    public string ContactType { get; set; }
}
