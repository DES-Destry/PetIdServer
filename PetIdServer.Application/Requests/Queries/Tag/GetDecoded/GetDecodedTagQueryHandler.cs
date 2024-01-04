using MediatR;
using PetIdServer.Application.Dto.Tag;
using PetIdServer.Application.Repositories;
using PetIdServer.Application.Services;
using PetIdServer.Core.Domains.Tag;
using PetIdServer.Core.Domains.Tag.Exceptions;

namespace PetIdServer.Application.Requests.Queries.Tag.GetDecoded;

public class GetDecodedTagQueryHandler(ITagRepository tagRepository, ICodeDecoder codeDecoder)
    : IRequestHandler<GetDecodedTagQuery, TagForAdminDto>
{
    public async Task<TagForAdminDto> Handle(GetDecodedTagQuery request, CancellationToken cancellationToken)
    {
        var tag = await tagRepository.GetTagById(new TagId(request.Id)) ?? throw new TagNotFoundException(
            $"Tag with Id {request.Id} not found", new
            {
                query = nameof(GetDecodedTagQuery),
                tagId = request.Id
            });

        var publicCode = await codeDecoder.GetPublicCodeOriginal(tag.Code);

        return new TagForAdminDto
        {
            Id = tag.Id.Value,
            PublicCode = publicCode,
            ControlCode = tag.ControlCode.ToString(),
            IsAlreadyInUse = tag.IsAlreadyInUse,
            CreatedAt = tag.CreatedAt,
            PetAddedAt = tag.PetAddedAt,
            LastScannedAt = tag.LastScannedAt
        };
    }
}