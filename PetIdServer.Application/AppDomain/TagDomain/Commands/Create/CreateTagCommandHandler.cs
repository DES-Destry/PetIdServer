using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Core.Domain.Tag;
using PetIdServer.Core.Domain.Tag.Exceptions;

namespace PetIdServer.Application.AppDomain.TagDomain.Commands.Create;

public class CreateTagCommandHandler(ITagRepository tagRepository)
    : IRequestHandler<CreateTagCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        CreateTagCommand request,
        CancellationToken cancellationToken)
    {
        await CheckDuplicates(request);

        var creationAttributes =
            new TagEntity.CreationAttributes((TagId) request.Id, request.Code);
        var tag = new TagEntity(creationAttributes);

        await tagRepository.CreateTag(tag);

        return VoidResponseDto.Executed;
    }

    private async Task CheckDuplicates(CreateTagCommand request)
    {
        var tagIdCandidate = await tagRepository.GetTagById((TagId) request.Id);

        if (tagIdCandidate is not null)
            throw new TagAlreadyInUseException(new
                {command = nameof(CreateTagCommand), tagId = request.Id});

        var tagCodeCandidate = await tagRepository.GetByCode(request.Code);

        if (tagCodeCandidate is not null)
            throw new TagAlreadyInUseException(new
                {command = nameof(CreateTagCommand), code = request.Code});
    }
}
