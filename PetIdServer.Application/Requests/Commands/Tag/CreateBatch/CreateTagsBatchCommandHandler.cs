using System.Collections.Immutable;
using MediatR;
using PetIdServer.Application.Dto;
using PetIdServer.Application.Repositories;
using PetIdServer.Core.Entities.Id;
using PetIdServer.Core.Exceptions.Common;
using PetIdServer.Core.Exceptions.Tag;

namespace PetIdServer.Application.Requests.Commands.Tag.CreateBatch;

public class CreateTagsBatchCommandHandler
    (ITagRepository tagRepository) : IRequestHandler<CreateTagsBatchCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(CreateTagsBatchCommand request, CancellationToken cancellationToken)
    {
        await CheckDuplicates(request);

        var ids = Enumerable.Range(request.IdFrom, request.IdTo).ToList();

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
            var controlCode = Random.Shared.NextInt64();
            var creationAttributes =
                new Core.Entities.Tag.CreationAttributes(new TagId(id), codes[index], controlCode);

            return new Core.Entities.Tag(creationAttributes);
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