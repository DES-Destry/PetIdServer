using MediatR;
using PetIdServer.Application.Pet.Dto;
using PetIdServer.Application.Tag.Dto;
using PetIdServer.Core.Domain.Tag;
using PetIdServer.Core.Domain.Tag.Exceptions;

namespace PetIdServer.Application.Tag.Queries.ControlCheck;

public class TagControlCheckQueryHandler(ITagRepository tagRepository)
    : IRequestHandler<TagControlCheckQuery, CheckTagDto>
{
    public async Task<CheckTagDto> Handle(
        TagControlCheckQuery request,
        CancellationToken cancellationToken)
    {
        TagEntity? tag = await tagRepository.GetTagByControlCode(request.ControlCode) ??
                         throw new TagNotFoundException($"Invalid control code: {request.ControlCode}", new
                         {
                             UseCase = nameof(TagControlCheckQuery), controlCode = request.ControlCode
                         });

        bool isFree = !tag.IsAlreadyInUse;
        CheckPetDto? pet = tag.IsAlreadyInUse ? new CheckPetDto(tag.Pet!.User.Email, tag.Pet.Name) : null;

        return new CheckTagDto(tag.Id, pet, isFree);
    }
}
