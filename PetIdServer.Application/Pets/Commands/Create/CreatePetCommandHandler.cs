using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.Pets.Commands.Create;

public class CreatePetCommandHandler(IPetRepository petRepository)
    : IRequestHandler<CreatePetCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        CreatePetCommand request,
        CancellationToken cancellationToken)
    {
        Core.Pets.Pet.CreationAttributes creationAttributes = new(
            request.Type,
            request.Name,
            request.Sex,
            request.IsCastrated);
        Core.Pets.Pet pet = new(creationAttributes);

        await petRepository.CreatePet(pet);

        return VoidResponseDto.Executed;
    }
}
