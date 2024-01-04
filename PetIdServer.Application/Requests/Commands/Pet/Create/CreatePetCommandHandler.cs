using MediatR;
using PetIdServer.Application.Dto;
using PetIdServer.Application.Repositories;
using PetIdServer.Core.Domain.Pet;

namespace PetIdServer.Application.Requests.Commands.Pet.Create;

public class CreatePetCommandHandler(IPetRepository petRepository)
    : IRequestHandler<CreatePetCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        CreatePetCommand request,
        CancellationToken cancellationToken)
    {
        var creationAttributes = new Core.Domain.Pet.Pet.CreationAttributes(
            new PetId(Guid.NewGuid()),
            request.Type,
            request.Name,
            request.Sex,
            request.IsCastrated);
        var pet = new Core.Domain.Pet.Pet(creationAttributes);

        await petRepository.CreatePet(pet);

        return VoidResponseDto.Executed;
    }
}
