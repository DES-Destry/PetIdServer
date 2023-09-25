using AutoMapper;
using MediatR;
using PetIdServer.Application.Dto;
using PetIdServer.Application.Repositories;
using PetIdServer.Core.Exceptions.Owner;

namespace PetIdServer.Application.Requests.Commands.Owner.Update;

public class UpdateOwnerCommandHandler : IRequestHandler<UpdateOwnerCommand, VoidResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IOwnerRepository _ownerRepository;

    public UpdateOwnerCommandHandler(IMapper mapper, IOwnerRepository ownerRepository)
    {
        _mapper = mapper;
        _ownerRepository = ownerRepository;
    }

    public async Task<VoidResponseDto> Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
    {
        // Owner Id is a email
        var owner = await _ownerRepository.GetOwnerByEmail(request.Id) ??
                    throw new OwnerNotFoundException($"Owner with email {request.Id} not found",
                        new {Email = request.Id});
        var updatedOwner = _mapper.Map<UpdateOwnerCommand, Core.Entities.Owner>(request);

        await _ownerRepository.UpdateOwner(owner.Id, updatedOwner);
        
        return VoidResponseDto.Executed;
    }
}