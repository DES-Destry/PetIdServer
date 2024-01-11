using MediatR;
using PetIdServer.Application.AppDomain.AdminDomain;
using PetIdServer.Application.AppDomain.TagDomain;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Core.Domain.Admin;
using PetIdServer.Core.Domain.Admin.Exceptions;
using PetIdServer.Core.Domain.Tag;
using PetIdServer.Core.Domain.Tag.Exceptions;
using PetIdServer.Core.Domain.TagReport;

namespace PetIdServer.Application.AppDomain.TagReportDomain.Commands.Create;

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
        var admin = await adminRepository.GetAdminById((AdminId) request.AdminId) ??
                    throw new AdminNotFoundException("Authorized admin not found!", new
                    {
                        command = nameof(CreateTagReportCommand),
                        adminId = request.AdminId
                    });

        var reportedTag = await tagRepository.GetTagById((TagId) request.TagId) ??
                          throw new TagNotFoundException(new
                          {
                              command = nameof(CreateTagReportCommand),
                              tagId = request.TagId
                          });

        var creationAttributes = new TagReportEntity.CreationAttributes(reportedTag, admin);
        var report = new TagReportEntity(creationAttributes);

        await reportRepository.CreateReport(report);

        return VoidResponseDto.Executed;
    }
}
