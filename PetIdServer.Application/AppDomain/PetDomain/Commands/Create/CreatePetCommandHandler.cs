using MediatR;
using PetIdServer.Application.AppDomain.PetDomain.Dto;
using PetIdServer.Core.Domain.Pet;

namespace PetIdServer.Application.AppDomain.PetDomain.Commands.Create;

public class CreatePetCommandHandler(IPetRepository petRepository)
    : IRequestHandler<CreatePetCommand, CreatePetResponseDto>
{
    public async Task<CreatePetResponseDto> Handle(
        CreatePetCommand request,
        CancellationToken cancellationToken)
    {
        var creationAttributes = new PetEntity.CreationAttributes(
            request.Type,
            request.Name,
            request.Sex,
            request.IsCastrated,
            request.Description,
            request.Owner);
        var pet = new PetEntity(creationAttributes);

        await petRepository.CreatePet(pet);

        return new CreatePetResponseDto {PetId = pet.Id};
    }
}
