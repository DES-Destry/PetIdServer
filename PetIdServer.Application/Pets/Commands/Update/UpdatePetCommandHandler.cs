using AutoMapper;
using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Core.Pets;
using PetIdServer.Core.Pets.Exceptions;

namespace PetIdServer.Application.Pets.Commands.Update;

public class UpdatePetCommandHandler(IMapper mapper, IPetRepository petRepository)
    : IRequestHandler<UpdatePetCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        UpdatePetCommand request,
        CancellationToken cancellationToken)
    {
        Pet? pet = await petRepository.GetPetById((PetId)request.Id) ??
                   throw new PetNotFoundException(
                       $"Pet with Id {request.Id} not found",
                       new
                       {
                           request.Id
                       });

        Pet? updatedPet = mapper.Map<UpdatePetCommand, Pet>(request);
        await petRepository.UpdatePet(pet.Id, updatedPet);

        return VoidResponseDto.Executed;
    }
}
