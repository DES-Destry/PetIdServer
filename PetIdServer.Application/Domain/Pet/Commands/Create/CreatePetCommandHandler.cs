using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Core.Domain.Pet;

namespace PetIdServer.Application.Domain.Pet.Commands.Create;

public class CreatePetCommandHandler(IPetRepository petRepository)
    : IRequestHandler<CreatePetCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        CreatePetCommand request,
        CancellationToken cancellationToken)
    {
        PetEntity.CreationAttributes creationAttributes = new(
            request.Type,
            request.Name,
            request.Sex,
            request.IsCastrated);
        PetEntity pet = new(creationAttributes);

        await petRepository.CreatePet(pet);

        return VoidResponseDto.Executed;
    }
}
