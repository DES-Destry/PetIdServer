using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Common.Services;
using PetIdServer.Core.Domain.Tag;
using PetIdServer.Core.Domain.Tag.Exceptions;

namespace PetIdServer.Application.AppDomain.TagDomain.Commands.Create;

public class CreateTagCommandHandler(ITagRepository tagRepository, ICodeDecoder codeDecoder, IHashService hashService)
    : IRequestHandler<CreateTagCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        CreateTagCommand request,
        CancellationToken cancellationToken)
    {
        await CheckDuplicates(request);

        string privateCode = await codeDecoder.EncodePublicCode(request.Code);
        string hashCode = await hashService.Hash(request.Code);

        TagEntity.CreationAttributes creationAttributes = new((TagId)request.Id, privateCode, hashCode);
        TagEntity tag = new(creationAttributes);

        await tagRepository.CreateTag(tag);

        return VoidResponseDto.Executed;
    }

    private async Task CheckDuplicates(CreateTagCommand request)
    {
        TagEntity? tagIdCandidate = await tagRepository.GetTagById((TagId)request.Id);

        if (tagIdCandidate is not null)
        {
            throw new TagAlreadyInUseException(new
            {
                command = nameof(CreateTagCommand), tagId = request.Id
            });
        }

        TagEntity? tagCodeCandidate = await tagRepository.GetByCode(request.Code);

        if (tagCodeCandidate is not null)
        {
            throw new TagAlreadyInUseException(new
            {
                command = nameof(CreateTagCommand), code = request.Code
            });
        }
    }
}
