using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Core.Pets;
using PetIdServer.Core.Users;

namespace PetIdServer.Application.Pets.Commands.Create;

public class CreatePetCommandHandler(IPetRepository petRepository)
    : IRequestHandler<CreatePetCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        CreatePetCommand request,
        CancellationToken cancellationToken)
    {
        Pet.CreationAttributes creationAttributes = new(
            (UserId)request.OwnerId,
            request.Name,
            request.Type,
            request.Sex,
            request.IsCastrated,
            request.PhotoId,
            request.Description);

        Pet pet = Pet.CreateNew(creationAttributes);
        await petRepository.CreatePet(pet);

        return VoidResponseDto.Executed;
    }
}
