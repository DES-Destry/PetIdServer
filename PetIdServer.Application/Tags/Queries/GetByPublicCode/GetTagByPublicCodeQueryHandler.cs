using MediatR;
using PetIdServer.Application.Common.Services;
using PetIdServer.Application.Pets;
using PetIdServer.Application.Tags.Dto;
using PetIdServer.Core.Pets;
using PetIdServer.Core.Pets.Exceptions;
using PetIdServer.Core.Tags;
using PetIdServer.Core.Tags.Exceptions;

namespace PetIdServer.Application.Tags.Queries.GetByPublicCode;

public class GetTagByPublicCodeQueryHandler(ITagRepository tagRepository, IPetRepository petRepository, IHashService hashService)
    : IRequestHandler<GetTagByPublicCodeQuery, TagDto>
{
    public async Task<TagDto> Handle(GetTagByPublicCodeQuery request, CancellationToken cancellationToken)
    {
        string hashCode = await hashService.Hash(request.Code);
        Tag tag = await tagRepository.GetTagByHashCode(hashCode) ??
                  throw new TagNotFoundException($"Tag with code {request.Code} not found", new
                  {
                      request.Code,
                      UseCase = nameof(GetTagByPublicCodeQuery)
                  });

        if (tag.IsAlreadyInUse)
        {
            Pet pet = await petRepository.GetPetById(tag.PetId!) ??
                      throw new PetNotFoundException("Tag is used, but pet within it is not found", new
                      {
                          TagId = tag.Id,
                          tag.PetId,
                          UseCase = nameof(GetTagByPublicCodeQuery)
                      });

            return TagDto.FromTagWithPet(tag, pet);
        }

        return TagDto.FromEmptyTag(tag);
    }
}
