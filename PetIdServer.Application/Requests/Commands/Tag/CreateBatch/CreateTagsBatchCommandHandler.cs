using MediatR;
using PetIdServer.Application.Dto;
using PetIdServer.Application.Repositories;
using PetIdServer.Core.Entities.Id;
using PetIdServer.Core.Exceptions.Tag;

namespace PetIdServer.Application.Requests.Commands.Tag.CreateBatch;

public class CreateTagsBatchCommandHandler(ITagRepository tagRepository) : IRequestHandler<CreateTagsBatchCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(CreateTagsBatchCommand request, CancellationToken cancellationToken)
    {
        await CheckDuplicates(request);
        
        var ids = Enumerable.Range(request.IdFrom, request.IdTo).ToList();
        
        if (ids.Count != request.Codes.Count())
            throw new Exception();

        using var codeEnumerator = request.Codes.GetEnumerator();
        var tags = ids.Select(id =>
        {
            var controlCode = Random.Shared.NextInt64();
            var creationAttributes =
                new Core.Entities.Tag.CreationAttributes(new TagId(id), codeEnumerator.Current, controlCode);

            codeEnumerator.MoveNext();

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