using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.Domain.Pet.Commands.Update;

public class UpdatePetCommand : IRequest<VoidResponseDto>
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public bool Sex { get; set; }
    public bool IsCastrated { get; set; }
    public string Description { get; set; }
}
