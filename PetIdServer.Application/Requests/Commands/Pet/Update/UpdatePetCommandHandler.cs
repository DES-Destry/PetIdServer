using AutoMapper;
using MediatR;
using PetIdServer.Application.Dto;
using PetIdServer.Application.Repositories;
using PetIdServer.Core.Exceptions.Pet;

namespace PetIdServer.Application.Requests.Commands.Pet.Update;

public class UpdatePetCommandHandler : IRequestHandler<UpdatePetCommand, VoidResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IPetRepository _petRepository;

    public UpdatePetCommandHandler(IMapper mapper, IPetRepository petRepository)
    {
        _mapper = mapper;
        _petRepository = petRepository;
    }

    public async Task<VoidResponseDto> Handle(UpdatePetCommand request, CancellationToken cancellationToken)
    {
        var pet = await _petRepository.GetPetById(request.Id) ??
                  throw new PetNotFoundException($"Pet with Id {request.Id} not found", new {Id = request.Id});
        var updatedPet = _mapper.Map<UpdatePetCommand, Core.Entities.Pet>(request);

        await _petRepository.UpdatePet(pet.Id, updatedPet);
        
        return VoidResponseDto.Executed;
    }
}