using AutoMapper;
using MediatR;
using PetIdServer.Application.AppDomain.PetDomain.Dto;
using PetIdServer.Application.AppDomain.TagDomain;
using PetIdServer.Core.Domain.Pet;
using PetIdServer.Core.Domain.Pet.Exceptions;
using PetIdServer.Core.Domain.Tag.Exceptions;

namespace PetIdServer.Application.AppDomain.PetDomain.Queries;

public class GetPetByCodeQueryHandler(ITagRepository tagRepository, IMapper mapper)
    : IRequestHandler<GetPetByCodeQuery, PetDto>
{
    public async Task<PetDto> Handle(
        GetPetByCodeQuery request,
        CancellationToken cancellationToken)
    {
        var tag = await tagRepository.GetByCode(request.PublicCode) ??
                  throw new TagNotFoundException("Tag with this code doesn't exists", new
                  {
                      Query = nameof(GetPetByCodeQueryHandler),
                      Code = request.PublicCode
                  });

        var pet = tag.Pet ??
                  throw new PetNotFoundException($"Tag {tag.Id.Value} has no attached pet!",
                      new {Query = nameof(GetPetByCodeQueryHandler), TagId = tag.Id.Value});

        return mapper.Map<PetEntity, PetDto>(pet);
    }
}
