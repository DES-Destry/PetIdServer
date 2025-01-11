using AutoMapper;
using MediatR;
using PetIdServer.Application.Common.Services;
using PetIdServer.Application.Tags.Dto;
using PetIdServer.Core.Tags;
using PetIdServer.Core.Tags.Exceptions;

namespace PetIdServer.Application.Tags.Queries.GetByPublicCode;

public class GetTagByPublicCodeQueryHandler(ITagRepository tagRepository, IHashService hashService, IMapper mapper)
    : IRequestHandler<GetTagByPublicCodeQuery, TagDto>
{
    public async Task<TagDto> Handle(GetTagByPublicCodeQuery request, CancellationToken cancellationToken)
    {
        string hashCode = await hashService.Hash(request.Code);
        Tag tag = await tagRepository.GetTagByHashCode(hashCode) ??
                  throw new TagNotFoundException($"Tag with code {request.Code} not found", new
                  {
                      request.Code, UseCase = nameof(GetTagByPublicCodeQuery)
                  });

        return mapper.Map<TagDto>(tag);
    }
}
