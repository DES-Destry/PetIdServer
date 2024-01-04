using MediatR;
using PetIdServer.Application.Dto;
using PetIdServer.Application.Repositories;
using PetIdServer.Core.Domains.Tag;
using PetIdServer.Core.Domains.Tag.Exceptions;

namespace PetIdServer.Application.Requests.Commands.Tag.Create;

public class CreateTagCommandHandler(ITagRepository tagRepository) : IRequestHandler<CreateTagCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        await CheckDuplicates(request);

        var controlCode = Random.Shared.NextInt64();

        var creationAttributes =
            new Core.Domains.Tag.Tag.CreationAttributes(new TagId(request.Id), request.Code, controlCode);
        var tag = new Core.Domains.Tag.Tag(creationAttributes);

        await tagRepository.CreateTag(tag);

        return VoidResponseDto.Executed;
    }

    private async Task CheckDuplicates(CreateTagCommand request)
    {
        var tagIdCandidate = await tagRepository.GetTagById(new TagId(request.Id));

        if (tagIdCandidate is not null)
            throw new TagAlreadyInUseException(new { command = nameof(CreateTagCommand), tagId = request.Id });

        var tagCodeCandidate = await tagRepository.GetByCode(request.Code);

        if (tagCodeCandidate is not null)
            throw new TagAlreadyInUseException(new { command = nameof(CreateTagCommand), code = request.Code });
    }
}