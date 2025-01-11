using MediatR;
using PetIdServer.Application.Tags.Dto;
using PetIdServer.Application.Tags.Services;
using PetIdServer.Core.Tags;
using PetIdServer.Core.Tags.Exceptions;

namespace PetIdServer.Application.Tags.Queries.GetDecoded;

public class GetDecodedTagQueryHandler(ITagRepository tagRepository, ICodeDecoder codeDecoder)
    : IRequestHandler<GetDecodedTagQuery, TagForAdminDto>
{
    public async Task<TagForAdminDto> Handle(
        GetDecodedTagQuery request,
        CancellationToken cancellationToken)
    {
        Tag tag = await tagRepository.GetTagById((TagId)request.Id) ??
                  throw new TagNotFoundException(
                      $"Tag with Id {request.Id} not found", new
                      {
                          UseCase = nameof(GetDecodedTagQuery), TagId = request.Id
                      });

        string publicCode = await codeDecoder.GetPublicCodeOriginal(tag.PrivateCode);

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
