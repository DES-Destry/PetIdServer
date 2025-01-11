using MediatR;
using PetIdServer.Application.Pets.Dto;
using PetIdServer.Application.Tags.Dto;
using PetIdServer.Core.Tags;
using PetIdServer.Core.Tags.Exceptions;

namespace PetIdServer.Application.Tags.Queries.ControlCheck;

public class TagControlCheckQueryHandler(ITagRepository tagRepository)
    : IRequestHandler<TagControlCheckQuery, CheckTagDto>
{
    public async Task<CheckTagDto> Handle(
        TagControlCheckQuery request,
        CancellationToken cancellationToken)
    {
        Tag? tag = await tagRepository.GetTagByControlCode(request.ControlCode) ??
                   throw new TagNotFoundException($"Invalid control code: {request.ControlCode}", new
                   {
                       UseCase = nameof(TagControlCheckQuery), controlCode = request.ControlCode
                   });

        bool isFree = !tag.IsAlreadyInUse;
        CheckPetDto? pet = tag.IsAlreadyInUse ? new CheckPetDto(tag.Pet!.User.Email, tag.Pet.Name) : null;

        return new CheckTagDto(tag.Id, pet, isFree);
    }
}
