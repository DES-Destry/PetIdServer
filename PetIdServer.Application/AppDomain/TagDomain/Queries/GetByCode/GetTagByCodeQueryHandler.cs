using MediatR;
using PetIdServer.Application.AppDomain.PetDomain.Dto;
using PetIdServer.Application.AppDomain.TagDomain.Dto;
using PetIdServer.Core.Domain.Tag.Exceptions;

namespace PetIdServer.Application.AppDomain.TagDomain.Queries.GetByCode;

public class GetTagByCodeQueryHandler(ITagRepository tagRepository)
    : IRequestHandler<GetTagByCodeQuery, CheckTagDto>
{
    public async Task<CheckTagDto> Handle(
        GetTagByCodeQuery request,
        CancellationToken cancellationToken)
    {
        var tag = await tagRepository.GetByCode(request.Code) ??
                  throw new TagNotFoundException("Tag with this code doesn't exists", new
                  {
                      Query = nameof(GetTagByCodeQuery), request.Code
                  });

        var pet = tag.IsAlreadyInUse ? new CheckPetDto(tag.Pet!.Owner.Email, tag.Pet.Name) : null;
        return new CheckTagDto(tag.Id, pet, !tag.IsAlreadyInUse);
    }
}
