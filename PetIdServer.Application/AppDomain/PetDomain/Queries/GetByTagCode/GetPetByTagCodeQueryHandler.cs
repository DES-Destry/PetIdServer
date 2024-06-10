using AutoMapper;
using MediatR;
using PetIdServer.Application.AppDomain.PetDomain.Dto;
using PetIdServer.Application.AppDomain.TagDomain;
using PetIdServer.Core.Domain.Pet;
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
                      Query = nameof(GetPetByTagCodeQueryHandler), request.Code
                  });

        var pet = tag.Pet ??
                  throw new PetNotFoundException($"Tag {tag.Id.Value} has no attached pet!",
                      new {Query = nameof(GetPetByTagCodeQueryHandler), TagId = tag.Id.Value});

        return mapper.Map<PetEntity, PetDto>(pet);
    }
}
