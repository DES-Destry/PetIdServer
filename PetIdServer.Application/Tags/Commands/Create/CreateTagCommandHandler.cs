using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Common.Services;
using PetIdServer.Application.Tags.Services;
using PetIdServer.Core.Tags;
using PetIdServer.Core.Tags.Exceptions;

namespace PetIdServer.Application.Tags.Commands.Create;

public class CreateTagCommandHandler(ITagRepository tagRepository, ICodeDecoder codeDecoder, IHashService hashService)
    : IRequestHandler<CreateTagCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        CreateTagCommand request,
        CancellationToken cancellationToken)
    {
        string hashCode = await hashService.Hash(request.Code);
        await CheckDuplicates(request, hashCode);

        string privateCode = await codeDecoder.EncodePublicCode(request.Code);

        Tag.CreationAttributes creationAttributes = new((TagId)request.Id, privateCode, hashCode);
        Tag tag = new(creationAttributes);

        await tagRepository.CreateTag(tag);

        return VoidResponseDto.Executed;
    }

    private async Task CheckDuplicates(CreateTagCommand request, string hashCode)
    {
        Tag? tagIdCandidate = await tagRepository.GetTagById((TagId)request.Id);

        if (tagIdCandidate is not null)
        {
            throw new TagAlreadyInUseException(new
            {
                command = nameof(CreateTagCommand), tagId = request.Id
            });
        }

        Tag? tagCodeCandidate = await tagRepository.GetTagByHashCode(hashCode);

        if (tagCodeCandidate is not null)
        {
            throw new TagAlreadyInUseException(new
            {
                command = nameof(CreateTagCommand), code = request.Code
            });
        }
    }
}
