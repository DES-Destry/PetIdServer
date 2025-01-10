using System.Collections.Immutable;
using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Domain.TagReport;
using PetIdServer.Core.Domain.Tag;
using PetIdServer.Core.Domain.Tag.Exceptions;
using PetIdServer.Core.Domain.TagReport;

namespace PetIdServer.Application.Domain.Tag.Commands.Clear;

public class ClearTagCommandHandler(
    ITagRepository tagRepository,
    ITagReportRepository tagReportRepository) : IRequestHandler<ClearTagCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        ClearTagCommand request,
        CancellationToken cancellationToken)
    {
        TagEntity tag = await tagRepository.GetTagById((TagId)request.TagId) ??
                        throw new TagNotFoundException(new
                        {
                            command = nameof(ClearTagCommand), reportId = request.TagId
                        });

        ImmutableArray<TagReportEntity> reports = [..await tagReportRepository.GetReportsByTagId(tag.Id)];

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

        await tagRepository.UpdateTag(tag.Id, tag);
        return VoidResponseDto.Executed;
    }
}
