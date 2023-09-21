using MediatR;
using PetIdServer.Application.Dto;
using PetIdServer.Application.Repositories;
using PetIdServer.Core.Entities.Id;

namespace PetIdServer.Application.Requests.Commands.Pet.Create;

public class CreatePetCommandHandler : IRequestHandler<CreatePetCommand, VoidResponseDto>
{
    private readonly IPetRepository _petRepository;

    public CreatePetCommandHandler(IPetRepository petRepository)
    {
        _petRepository = petRepository;
    }

    public async Task<VoidResponseDto> Handle(CreatePetCommand request, CancellationToken cancellationToken)
    {
        var creationAttributes = new Core.Entities.Pet.CreationAttributes(
            new PetId(Guid.NewGuid()),
            request.Type,
            request.Name,
            request.Sex,
            request.IsCastrated);
        var pet = new Core.Entities.Pet(creationAttributes);

        await _petRepository.CreatePet(pet);

        return VoidResponseDto.Executed;
    }
}