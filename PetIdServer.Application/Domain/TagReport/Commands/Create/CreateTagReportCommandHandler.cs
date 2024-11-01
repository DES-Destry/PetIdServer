using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Domain.Admin;
using PetIdServer.Application.Domain.Tag;
using PetIdServer.Core.Domain.Admin;
using PetIdServer.Core.Domain.Admin.Exceptions;
using PetIdServer.Core.Domain.Tag;
using PetIdServer.Core.Domain.Tag.Exceptions;
using PetIdServer.Core.Domain.TagReport;

namespace PetIdServer.Application.Domain.TagReport.Commands.Create;

public class CreateTagReportCommandHandler(
    IAdminRepository adminRepository,
    ITagRepository tagRepository,
    ITagReportRepository reportRepository)
    : IRequestHandler<CreateTagReportCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        CreateTagReportCommand request,
        CancellationToken cancellationToken)
    {
        AdminEntity? admin = await adminRepository.GetAdminById((AdminId)request.AdminId) ??
                             throw new AdminNotFoundException("Authorized admin not found!", new
                             {
                                 command = nameof(CreateTagReportCommand), adminId = request.AdminId
                             });

        TagEntity? reportedTag = await tagRepository.GetTagById((TagId)request.TagId) ??
                                 throw new TagNotFoundException(new
                                 {
                                     command = nameof(CreateTagReportCommand), tagId = request.TagId
                                 });

        TagReportEntity.CreationAttributes? creationAttributes = new(reportedTag, admin);
        TagReportEntity? report = new(creationAttributes);

        await reportRepository.CreateReport(report);

        return VoidResponseDto.Executed;
    }
}
