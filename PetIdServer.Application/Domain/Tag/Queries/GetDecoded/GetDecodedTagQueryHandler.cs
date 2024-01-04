using MediatR;
using PetIdServer.Application.Common.Services;
using PetIdServer.Application.Domain.Tag.Dto;
using PetIdServer.Core.Domain.Tag;
using PetIdServer.Core.Domain.Tag.Exceptions;

namespace PetIdServer.Application.Domain.Tag.Queries.GetDecoded;

public class GetDecodedTagQueryHandler(ITagRepository tagRepository, ICodeDecoder codeDecoder)
    : IRequestHandler<GetDecodedTagQuery, TagForAdminDto>
{
    public async Task<TagForAdminDto> Handle(
        GetDecodedTagQuery request,
        CancellationToken cancellationToken)
    {
        var tag = await tagRepository.GetTagById(new TagId(request.Id)) ??
                  throw new TagNotFoundException(
                      $"Tag with Id {request.Id} not found", new
                      {
                          query = nameof(GetDecodedTagQuery),
                          tagId = request.Id
                      });

        var publicCode = await codeDecoder.GetPublicCodeOriginal(tag.Code);

        return new TagForAdminDto(
            tag.Id,
            publicCode,
            tag.ControlCode.ToString(),
            tag.IsAlreadyInUse,
            tag.CreatedAt,
            tag.PetAddedAt,
            tag.LastScannedAt);
    }
}
