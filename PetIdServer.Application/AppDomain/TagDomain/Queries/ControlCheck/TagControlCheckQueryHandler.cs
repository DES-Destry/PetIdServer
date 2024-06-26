using MediatR;
using PetIdServer.Application.AppDomain.PetDomain.Dto;
using PetIdServer.Application.AppDomain.TagDomain.Dto;
using PetIdServer.Core.Domain.Tag.Exceptions;

namespace PetIdServer.Application.AppDomain.TagDomain.Queries.ControlCheck;

public class TagControlCheckQueryHandler(ITagRepository tagRepository)
    : IRequestHandler<TagControlCheckQuery, CheckTagDto>
{
    public async Task<CheckTagDto> Handle(
        TagControlCheckQuery request,
        CancellationToken cancellationToken)
    {
        var tag = await tagRepository.GetByControlCode(request.ControlCode);

        if (tag is null)
            throw new TagNotFoundException($"Invalid control code: {request.ControlCode}", new
            {
                query = nameof(TagControlCheckQuery),
                controlCode = request.ControlCode
            });

        var isFree = !tag.IsAlreadyInUse;
        var pet = tag.IsAlreadyInUse ? new CheckPetDto(tag.Pet!.Owner.Email, tag.Pet.Name) : null;

        return new CheckTagDto(tag.Id, pet, isFree);
    }
}
