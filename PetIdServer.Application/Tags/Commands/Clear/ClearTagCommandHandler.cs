using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Core.Tags;
using PetIdServer.Core.Tags.Exceptions;
using PetIdServer.Core.Users;

namespace PetIdServer.Application.Tags.Commands.Clear;

public class ClearTagCommandHandler(ITagRepository tagRepository) : IRequestHandler<ClearTagCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        ClearTagCommand request,
        CancellationToken cancellationToken)
    {
        Tag tag = await tagRepository.GetTagById((TagId)request.TagId) ??
                  throw new TagNotFoundException(new
                  {
                      Command = nameof(ClearTagCommand), request.TagId
                  });

        if (tag.IsAlreadyInUse && tag.Reports.All(report => report.IsResolved))
        {
            throw new TagCannotBeClearedException(new
            {
                Command = nameof(ClearTagCommand),
                request.TagId,
                TagIsAlreadyInUse = tag.IsAlreadyInUse,
                ReportsCount = tag.Reports.Count,
                NotResolvedReportsCount = 0
            });
        }

        tag.RemovePet(new UserId(request.AdminId));

        await tagRepository.UpdateTag(tag);
        return VoidResponseDto.Executed;
    }
}
