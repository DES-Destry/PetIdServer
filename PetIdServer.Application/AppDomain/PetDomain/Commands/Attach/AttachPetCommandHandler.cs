using MediatR;
using PetIdServer.Application.AppDomain.TagDomain;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Core.Domain.Pet;
using PetIdServer.Core.Domain.Pet.Exceptions;
using PetIdServer.Core.Domain.Tag;
using PetIdServer.Core.Domain.Tag.Exceptions;

namespace PetIdServer.Application.AppDomain.PetDomain.Commands.Attach;

public class AttachPetCommandHandler(IPetRepository petRepository, ITagRepository tagRepository)
    : IRequestHandler<AttachPetCommand, VoidResponseDto>
{
    // TODO make request authorized
    public async Task<VoidResponseDto> Handle(
        AttachPetCommand request,
        CancellationToken cancellationToken)
    {
        var pet = await petRepository.GetPetById(new PetId(request.PetId)) ??
                  throw new PetNotFoundException($"Pet with Id {request.PetId} not found!",
                      new {Command = nameof(AttachPetCommand), request.PetId});

        var tag = await tagRepository.GetTagById(new TagId(request.TagId)) ??
                  throw new TagNotFoundException("Tag with this Id doesn't exists", new
                  {
                      Command = nameof(AttachPetCommand), request.TagId
                  });

        // TODO why domain logic is in repository...
        await tagRepository.AttachPet(tag.Id, pet);

        return VoidResponseDto.Executed;
    }
}
