using AutoMapper;
using MediatR;
using PetIdServer.Application.AppDomain.TagDomain;
using PetIdServer.Application.Common.Services.Dto;
using PetIdServer.Core.Domain.Pet.Exceptions;
using PetIdServer.Core.Domain.Tag.Exceptions;

namespace PetIdServer.Application.AppDomain.PetDomain.Queries.GetByTagCode;

public class GetPetByTagCodeQueryHandler(ITagRepository tagRepository, IMapper mapper)
    : IRequestHandler<GetPetByTagCodeQuery, PetDto>
{
    public async Task<PetDto> Handle(
        GetPetByTagCodeQuery request,
        CancellationToken cancellationToken)
    {
        var tag = await tagRepository.GetByCode(request.Code) ??
                  throw new TagNotFoundException("Tag with this code doesn't exists", new
                  {
                      Query = nameof(GetPetByTagCodeQuery), request.Code
                  });

        return tag.Pet ??
               throw new PetNotFoundException($"Tag {tag.Id.Value} has no attached pet!",
                   new {Query = nameof(GetPetByTagCodeQuery), TagId = tag.Id.Value});
    }
}
