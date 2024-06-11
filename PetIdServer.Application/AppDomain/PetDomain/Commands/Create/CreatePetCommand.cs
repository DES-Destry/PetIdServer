using MediatR;
using PetIdServer.Application.AppDomain.PetDomain.Dto;
using PetIdServer.Core.Domain.Owner;

namespace PetIdServer.Application.AppDomain.PetDomain.Commands.Create;

public class CreatePetCommand : IRequest<CreatePetResponseDto>
{
    public string Type { get; set; }
    public string Name { get; set; }
    public bool Sex { get; set; }
    public bool IsCastrated { get; set; }
    public string Description { get; set; }

    // Authorized field
    public OwnerEntity Owner { get; set; }
}
