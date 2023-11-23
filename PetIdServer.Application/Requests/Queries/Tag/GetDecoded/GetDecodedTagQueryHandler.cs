using MediatR;
using PetIdServer.Application.Dto.Tag;
using PetIdServer.Application.Repositories;
using PetIdServer.Application.Services;
using PetIdServer.Core.Entities.Id;
using PetIdServer.Core.Exceptions.Tag;

namespace PetIdServer.Application.Requests.Queries.Tag.GetDecoded;

public class GetDecodedTagQueryHandler
    (ITagRepository tagRepository, ICodeDecoder codeDecoder) : IRequestHandler<GetDecodedTagQuery, TagForAdminDto>
{
    public async Task<TagForAdminDto> Handle(GetDecodedTagQuery request, CancellationToken cancellationToken)
    {
        var tag = await tagRepository.GetTagById(new TagId(request.Id)) ?? throw new TagNotFoundException(
            $"Tag with Id {request.Id} not found", new
            {
                query = nameof(GetDecodedTagQuery),
                tagId = request.Id,
            });

        var publicCode = await codeDecoder.GetPublicCodeOriginal(tag.Code);

        return new TagForAdminDto
        {
            Id = tag.Id.Value,
            PublicCode = publicCode,
            ControlCode = tag.ControlCode,
            IsAlreadyInUse = tag.IsAlreadyInUse,
        };
    }
}