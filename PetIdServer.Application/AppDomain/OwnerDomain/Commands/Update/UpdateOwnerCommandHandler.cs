using AutoMapper;
using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Core.Domain.Owner;
using PetIdServer.Core.Domain.Owner.Exceptions;

namespace PetIdServer.Application.AppDomain.OwnerDomain.Commands.Update;

public class UpdateOwnerCommandHandler(IMapper mapper, IOwnerRepository ownerRepository)
    : IRequestHandler<UpdateOwnerCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        UpdateOwnerCommand request,
        CancellationToken cancellationToken)
    {
        // Owner Id is a email
        var owner = await ownerRepository.GetOwnerByEmail(request.Id) ??
                    throw new OwnerNotFoundException($"Owner with email {request.Id} not found",
                        new {Email = request.Id});
        var updatedOwner = mapper.Map<UpdateOwnerCommand, OwnerEntity>(request);

        await ownerRepository.UpdateOwner(owner.Id, updatedOwner);

        return VoidResponseDto.Executed;
    }
}
