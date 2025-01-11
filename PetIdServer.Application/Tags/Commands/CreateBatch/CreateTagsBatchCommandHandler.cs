using System.Collections.Immutable;
using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Common.Services;
using PetIdServer.Application.Tags.Services;
using PetIdServer.Core.Common.Exceptions;
using PetIdServer.Core.Tags;
using PetIdServer.Core.Tags.Exceptions;

namespace PetIdServer.Application.Tags.Commands.CreateBatch;

public class CreateTagsBatchCommandHandler(ITagRepository tagRepository, ICodeDecoder codeDecoder, IHashService hashService)
    : IRequestHandler<CreateTagsBatchCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        CreateTagsBatchCommand request,
        CancellationToken cancellationToken)
    {
        int tagsCount = request.IdTo - request.IdFrom + 1;

        ImmutableArray<string> hashCodes =
            [..await Task.WhenAll(request.Codes.Select(async code => await hashService.Hash(code)))];

        await CheckDuplicates(request, hashCodes);

        ImmutableArray<int> ids = [..Enumerable.Range(request.IdFrom, tagsCount)];

        if (ids.Length != request.Codes.Count())
        {
            throw new ValidationException("Id range must be same with codes count", new
            {
                UseCase = nameof(CreateTagsBatchCommand),
                request.IdFrom,
                request.IdTo,
                IdsCount = ids.Length,
                CodesCount = request.Codes.Count()
            });
        }

        ImmutableArray<string> codes = [..request.Codes];
        Tag[] tags = new Tag[ids.Length];

        foreach ((int index, string code) in codes.Index())
        {
            string privateCode = await codeDecoder.EncodePublicCode(code);
            string hashCode = await hashService.Hash(code);

            Tag.CreationAttributes creationAttributes = new((TagId)index, privateCode, hashCode);
            tags[index] = new Tag(creationAttributes);
        }

        await tagRepository.CreateTagsBatch(tags);

        return VoidResponseDto.Executed;
    }

    private async Task CheckDuplicates(CreateTagsBatchCommand request, ImmutableArray<string> hashCodes)
    {
        IEnumerable<int> ids = Enumerable.Range(request.IdFrom, request.IdTo);
        bool areIdsAvailable = await tagRepository.AreIdsAvailable(ids);

        if (!areIdsAvailable)
        {
            throw new TagAlreadyCreatedException(new
            {
                UseCase = nameof(CreateTagsBatchCommand), ConflictReason = "Some of the ids are already in use"
            });
        }

        bool areCodesAvailable = await tagRepository.AreHashCodesAvailable(hashCodes);

        if (!areCodesAvailable)
        {
            throw new TagAlreadyCreatedException(new
            {
                UseCase = nameof(CreateTagsBatchCommand), ConflictReason = "Some of the codes are already in use"
            });
        }
    }
}
