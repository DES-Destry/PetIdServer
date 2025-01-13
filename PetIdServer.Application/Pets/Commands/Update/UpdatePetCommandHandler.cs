using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Pets.Mapper;
using PetIdServer.Core.Pets;
using PetIdServer.Core.Pets.Exceptions;

namespace PetIdServer.Application.Pets.Commands.Update;

public class UpdatePetCommandHandler(IPetRepository petRepository)
    : IRequestHandler<UpdatePetCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        UpdatePetCommand request,
        CancellationToken cancellationToken)
    {
        Pet pet = await petRepository.GetPetById((PetId)request.Id) ??
                  throw new PetNotFoundException(
                      $"Pet with Id {request.Id} not found",
                      new
                      {
                          request.Id
                      });

        pet = pet.MapUpdate(request);
        await petRepository.UpdatePet(pet.Id, pet);

        return VoidResponseDto.Executed;
    }
}
