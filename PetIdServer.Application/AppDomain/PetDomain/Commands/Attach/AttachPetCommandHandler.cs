using MediatR;
using PetIdServer.Application.AppDomain.TagDomain;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Core.Domain.Pet;
using PetIdServer.Core.Domain.Pet.Exceptions;
using PetIdServer.Core.Domain.Tag.Exceptions;

namespace PetIdServer.Application.AppDomain.PetDomain.Commands.Attach;

public class AttachPetCommandHandler(IPetRepository petRepository, ITagRepository tagRepository)
    : IRequestHandler<AttachPetCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        AttachPetCommand request,
        CancellationToken cancellationToken)
    {
        var pet = await petRepository.GetPetById(new PetId(request.PetId)) ??
                  throw new PetNotFoundException($"Pet with Id {request.PetId} not found!",
                      new {Command = nameof(AttachPetCommand), request.PetId});

        var tag = await tagRepository.GetByCode(request.TagCode) ??
                  throw new TagNotFoundException("Tag with this code doesn't exists", new
                  {
                      Command = nameof(AttachPetCommand), request.TagCode
                  });

        // TODO why domain logic is in repository...
        await tagRepository.AttachPet(tag.Id, pet);

        return VoidResponseDto.Executed;
    }
}
