using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Core.Domain.Pet;

namespace PetIdServer.Application.AppDomain.PetDomain.Commands.Create;

public class CreatePetCommandHandler(IPetRepository petRepository)
    : IRequestHandler<CreatePetCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        CreatePetCommand request,
        CancellationToken cancellationToken)
    {
        var creationAttributes = new PetEntity.CreationAttributes(
            request.Type,
            request.Name,
            request.Sex,
            request.IsCastrated);
        var pet = new PetEntity(creationAttributes);

        await petRepository.CreatePet(pet);

        return VoidResponseDto.Executed;
    }
}