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

        if (tagsCount != request.Codes.Count())
        {
            throw new ValidationException("Id range must be same with codes count", new
            {
                UseCase = nameof(CreateTagsBatchCommand),
                request.IdFrom,
                request.IdTo,
                ExpectedCount = tagsCount,
                ActualCodesCount = request.Codes.Count()
            });
        }

        ImmutableArray<int> ids = [..Enumerable.Range(request.IdFrom, tagsCount)];
        ImmutableArray<string> hashCodes =
            [..await Task.WhenAll(request.Codes.Select(async code => await hashService.Hash(code)))];

        await EnsureNoDuplicatesAsync(ids, hashCodes);

        Tag[] tags = await Task.WhenAll(request.Codes.Select(async (code, index) =>
        {
            string privateCode = await codeDecoder.EncodePublicCode(code);
            string hashCode = hashCodes[index];

            Tag.CreationAttributes creationAttributes = new((TagId)ids[index], privateCode, hashCode);
            return Tag.CreateNew(creationAttributes);
        }));

        await tagRepository.CreateTagsBatch(tags);

        return VoidResponseDto.Executed;
    }

    private async Task EnsureNoDuplicatesAsync(ImmutableArray<int> ids, ImmutableArray<string> hashCodes)
    {
        TagId[] tagIds = ids.Select(id => (TagId)id).ToArray();
        Task<bool>[] checks = [tagRepository.AreIdsAvailable(tagIds), tagRepository.AreHashCodesAvailable(hashCodes)];
        bool[] results = await Task.WhenAll(checks);

        if (!results[0])
        {
            throw new TagAlreadyCreatedException(new
            {
                UseCase = nameof(CreateTagsBatchCommand), ConflictReason = "Some of the ids are already in use"
            });
        }

        if (!results[1])
        {
            throw new TagAlreadyCreatedException(new
            {
                UseCase = nameof(CreateTagsBatchCommand), ConflictReason = "Some of the codes are already in use"
            });
        }
    }
}
