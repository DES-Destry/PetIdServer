using System.Collections.Immutable;
using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.TagReports;
using PetIdServer.Core.TagReports;
using PetIdServer.Core.Tags;
using PetIdServer.Core.Tags.Exceptions;

namespace PetIdServer.Application.Tags.Commands.Clear;

public class ClearTagCommandHandler(
    ITagRepository tagRepository,
    ITagReportRepository tagReportRepository) : IRequestHandler<ClearTagCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        ClearTagCommand request,
        CancellationToken cancellationToken)
    {
        Tag tag = await tagRepository.GetTagById((TagId)request.TagId) ??
                  throw new TagNotFoundException(new
                  {
                      command = nameof(ClearTagCommand), reportId = request.TagId
                  });

        ImmutableArray<TagReport> reports = [..await tagReportRepository.GetReportsByTagId(tag.Id)];

        if (tag.IsAlreadyInUse && reports.All(report => report.IsResolved))
        {
            throw new TagCannotBeClearedException(new
            {
                command = nameof(ClearTagCommand),
                tagId = request.TagId,
                tagIsAlreadyInUse = tag.IsAlreadyInUse,
                tagReports = reports
            });
        }

        tag.RemovePet();

        await tagRepository.UpdateTag(tag);
        return VoidResponseDto.Executed;
    }
}
