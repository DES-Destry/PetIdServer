using MediatR;
using PetIdServer.Application.AppDomain.AdminDomain;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Core.Domain.Admin;
using PetIdServer.Core.Domain.Admin.Exceptions;
using PetIdServer.Core.Domain.TagReport;
using PetIdServer.Core.Domain.TagReport.Exceptions;

namespace PetIdServer.Application.AppDomain.TagReportDomain.Commands.Resolve;

public class ResolveTagReportCommandHandler(
    IAdminRepository adminRepository,
    ITagReportRepository tagReportRepository)
    : IRequestHandler<ResolveTagReportCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        ResolveTagReportCommand request,
        CancellationToken cancellationToken)
    {
        var admin = await adminRepository.GetAdminById((AdminId) request.AdminId) ??
                    throw new AdminNotFoundException("Authorized admin not found", new
                    {
                        command = nameof(ResolveTagReportCommand),
                        adminId = request.AdminId
                    });

        var report = await tagReportRepository.GetTagReportById((TagReportId) request.ReportId) ??
                     throw new TagReportNotFoundException(new
                     {
                         command = nameof(ResolveTagReportCommand),
                         reportId = request.ReportId
                     });

        report.ResolvedBy(admin);

        await tagReportRepository.UpdateReport(report.Id, report);
        return VoidResponseDto.Executed;
    }
}
