using AutoMapper;
using MediatR;
using PetIdServer.Application.Dto;
using PetIdServer.Application.Repositories;
using PetIdServer.Core.Domains.Owner.Exceptions;

namespace PetIdServer.Application.Requests.Commands.Owner.Update;

public class UpdateOwnerCommandHandler(IMapper mapper, IOwnerRepository ownerRepository)
    : IRequestHandler<UpdateOwnerCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
    {
        // Owner Id is a email
        var owner = await ownerRepository.GetOwnerByEmail(request.Id) ??
                    throw new OwnerNotFoundException($"Owner with email {request.Id} not found",
                        new { Email = request.Id });
        var updatedOwner = mapper.Map<UpdateOwnerCommand, Core.Domains.Owner.Owner>(request);

        await ownerRepository.UpdateOwner(owner.Id, updatedOwner);

        return VoidResponseDto.Executed;
    }
}