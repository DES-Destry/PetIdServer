using System.Collections.Immutable;
using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Core.Common.Exceptions.Common;
using PetIdServer.Core.Domain.Tag;
using PetIdServer.Core.Domain.Tag.Exceptions;

namespace PetIdServer.Application.AppDomain.TagDomain.Commands.CreateBatch;

public class CreateTagsBatchCommandHandler(ITagRepository tagRepository)
    : IRequestHandler<CreateTagsBatchCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        CreateTagsBatchCommand request,
        CancellationToken cancellationToken)
    {
        await CheckDuplicates(request);

        var tagsCount = request.IdTo - request.IdFrom + 1;
        var ids = Enumerable.Range(request.IdFrom, tagsCount).ToList();

        if (ids.Count != request.Codes.Count())
            throw new ValidationException("Id range must be same with codes count", new
            {
                command = nameof(CreateTagsBatchCommand),
                idFrom = request.IdFrom,
                idTo = request.IdTo,
                idsCount = ids.Count,
                codesCount = request.Codes.Count()
            });

        var codes = request.Codes.ToImmutableArray();

        var tags = ids.Select((id, index) =>
        {
            var creationAttributes =
                new TagEntity.CreationAttributes((TagId) id, codes[index]);
            return new TagEntity(creationAttributes);
        });

        await tagRepository.CreateTagsBatch(tags);

        return VoidResponseDto.Executed;
    }

    private async Task CheckDuplicates(CreateTagsBatchCommand request)
    {
        var ids = Enumerable.Range(request.IdFrom, request.IdTo);
        var idsAvailable = await tagRepository.IsIdsAvailable(ids);

        if (!idsAvailable)
            throw new TagAlreadyCreatedException(new {command = nameof(CreateTagsBatchCommand)});

        var codesAvailable = await tagRepository.IsCodesAvailable(request.Codes);

        if (!codesAvailable)
            throw new TagAlreadyCreatedException(new {command = nameof(CreateTagsBatchCommand)});
    }
}
