using System.Collections.Immutable;
using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Domain.Admin;
using PetIdServer.Application.Domain.TagReport;
using PetIdServer.Core.Domain.Admin;
using PetIdServer.Core.Domain.Admin.Exceptions;
using PetIdServer.Core.Domain.Tag;
using PetIdServer.Core.Domain.Tag.Exceptions;
using PetIdServer.Core.Domain.TagReport;

namespace PetIdServer.Application.Domain.Tag.Commands.Clear;

public class ClearTagCommandHandler(
    IAdminRepository adminRepository,
    ITagRepository tagRepository,
    ITagReportRepository tagReportRepository) : IRequestHandler<ClearTagCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        ClearTagCommand request,
        CancellationToken cancellationToken)
    {
        AdminEntity? admin = await adminRepository.GetAdminById((AdminId)request.AdminId) ??
                             throw new AdminNotFoundException("Authorized admin not found", new
                             {
                                 command = nameof(ClearTagCommand), adminId = request.AdminId
                             });

        TagEntity? tag = await tagRepository.GetTagById((TagId)request.TagId) ??
                         throw new TagNotFoundException(new
                         {
                             command = nameof(ClearTagCommand), reportId = request.TagId
                         });

        ImmutableArray<TagReportEntity> reports = (await tagReportRepository.GetReportsByTagId(tag.Id)).ToImmutableArray();

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
