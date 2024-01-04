using AutoMapper;
using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Core.Domain.Pet;
using PetIdServer.Core.Domain.Pet.Exceptions;

namespace PetIdServer.Application.Domain.Pet.Commands.Update;

public class UpdatePetCommandHandler(IMapper mapper, IPetRepository petRepository)
    : IRequestHandler<UpdatePetCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        UpdatePetCommand request,
        CancellationToken cancellationToken)
    {
        var pet = await petRepository.GetPetById(new PetId(request.Id)) ??
                  throw new PetNotFoundException($"Pet with Id {request.Id} not found",
                      new {request.Id});
        var updatedPet = mapper.Map<UpdatePetCommand, Core.Domain.Pet.Pet>(request);

        await petRepository.UpdatePet(pet.Id, updatedPet);

        return VoidResponseDto.Executed;
    }
}
