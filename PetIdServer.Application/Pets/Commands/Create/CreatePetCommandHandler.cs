using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Core.Pets;

namespace PetIdServer.Application.Pets.Commands.Create;

public class CreatePetCommandHandler(IPetRepository petRepository)
    : IRequestHandler<CreatePetCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        CreatePetCommand request,
        CancellationToken cancellationToken)
    {
        Pet.CreationAttributes creationAttributes = new(
            request.Type,
            request.Name,
            request.Sex,
            request.IsCastrated);
        Pet pet = new(creationAttributes);

        await petRepository.CreatePet(pet);

        return VoidResponseDto.Executed;
    }
}
