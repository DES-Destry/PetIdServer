using AutoMapper;
using MediatR;
using PetIdServer.Application.AppDomain.TagDomain;
using PetIdServer.Application.Common.Services.Dto;
using PetIdServer.Core.Domain.Pet.Exceptions;
using PetIdServer.Core.Domain.Tag;
using PetIdServer.Core.Domain.Tag.Exceptions;

namespace PetIdServer.Application.AppDomain.PetDomain.Queries.GetByTagId;

public class GetPetByTagIdQueryHandler(ITagRepository tagRepository, IMapper mapper)
    : IRequestHandler<GetPetByTagIdQuery, PetDto>
{
    public async Task<PetDto> Handle(
        GetPetByTagIdQuery request,
        CancellationToken cancellationToken)
    {
        var tag = await tagRepository.GetTagById(new TagId(request.TagId)) ??
                  throw new TagNotFoundException("Tag with this code doesn't exists", new
                  {
                      Query = nameof(GetPetByTagIdQuery), request.TagId
                  });

        return tag.Pet ??
               throw new PetNotFoundException($"Tag {request.TagId} has no attached pet!",
                   new {Query = nameof(GetPetByTagIdQuery), request.TagId});
    }
}
