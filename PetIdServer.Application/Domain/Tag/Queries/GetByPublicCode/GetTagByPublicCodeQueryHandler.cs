using AutoMapper;
using MediatR;
using PetIdServer.Application.Common.Services;
using PetIdServer.Application.Domain.Tag.Dto;
using PetIdServer.Core.Domain.Tag;
using PetIdServer.Core.Domain.Tag.Exceptions;

namespace PetIdServer.Application.Domain.Tag.Queries.GetByPublicCode;

public class GetTagByPublicCodeQueryHandler(ITagRepository tagRepository, IHashService hashService, IMapper mapper)
    : IRequestHandler<GetTagByPublicCodeQuery, TagDto>
{
    public async Task<TagDto> Handle(GetTagByPublicCodeQuery request, CancellationToken cancellationToken)
    {
        string hashCode = await hashService.Hash(request.Code);
        TagEntity tag = await tagRepository.GetTagByHashCode(hashCode) ??
                        throw new TagNotFoundException($"Tag with code {request.Code} not found", new
                        {
                            request.Code
                        });

        return mapper.Map<TagDto>(tag);
    }
}
