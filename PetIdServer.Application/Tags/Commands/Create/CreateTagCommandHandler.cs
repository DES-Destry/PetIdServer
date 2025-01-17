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
        await EnsureNoDuplicatesAsync((TagId)request.Id, hashCode);

        string privateCode = await codeDecoder.EncodePublicCode(request.Code);

        Tag.CreationAttributes creationAttributes = new((TagId)request.Id, privateCode, hashCode);
        Tag tag = Tag.CreateNew(creationAttributes);

        await tagRepository.CreateTag(tag);

        return VoidResponseDto.Executed;
    }

    private async Task EnsureNoDuplicatesAsync(TagId id, string hashCode)
    {
        Task<Tag?>[] checks = [tagRepository.GetTagById(id), tagRepository.GetTagByHashCode(hashCode)];
        Tag?[] results = await Task.WhenAll(checks);

        if (results[0] is not null)
        {
            throw new TagAlreadyInUseException("Tag with such Id is already exists", new
            {
                Command = nameof(CreateTagCommand), TagId = id
            });
        }

        if (results[1] is not null)
        {
            throw new TagAlreadyInUseException("Tag with such hash code is already exists", new
            {
                Command = nameof(CreateTagCommand), TagId = id
            });
        }
    }
}
