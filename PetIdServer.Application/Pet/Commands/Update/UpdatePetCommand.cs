using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.Pet.Commands.Update;

public class UpdatePetCommand : IRequest<VoidResponseDto>
{
    public required Guid Id { get; init; }
    public string? Type { get; init; }
    public string? Name { get; init; }
    public bool? Sex { get; init; }
    public bool? IsCastrated { get; init; }
    public string? Description { get; init; }
}
