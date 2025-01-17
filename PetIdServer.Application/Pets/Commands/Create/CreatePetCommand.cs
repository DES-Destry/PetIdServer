using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.Pets.Commands.Create;

public class CreatePetCommand : IRequest<VoidResponseDto>
{
    public required Guid OwnerId { get; set; }

    public required string Type { get; init; }
    public required string Name { get; init; }
    public required bool Sex { get; init; }
    public required bool IsCastrated { get; init; }

    public Guid PhotoId { get; init; }

    public string Description { get; init; }
}
