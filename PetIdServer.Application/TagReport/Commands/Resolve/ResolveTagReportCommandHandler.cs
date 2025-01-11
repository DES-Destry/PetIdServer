using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.User;
using PetIdServer.Core.Domain.TagReport;
using PetIdServer.Core.Domain.TagReport.Exceptions;
using PetIdServer.Core.Domain.User;
using PetIdServer.Core.Domain.User.Exceptions;

namespace PetIdServer.Application.TagReport.Commands.Resolve;

public class ResolveTagReportCommandHandler(
    IUserRepository userRepository,
    ITagReportRepository tagReportRepository)
    : IRequestHandler<ResolveTagReportCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        ResolveTagReportCommand request,
        CancellationToken cancellationToken)
    {
        UserEntity admin = await userRepository.GetUserById((UserId)request.AdminId) ??
                           throw new UserNotFoundException("Authorized admin not found", new
                           {
                               command = nameof(ResolveTagReportCommand), adminId = request.AdminId
                           });

        TagReportEntity report = await tagReportRepository.GetTagReportById((TagReportId)request.ReportId) ??
                                 throw new TagReportNotFoundException(new
                                 {
                                     command = nameof(ResolveTagReportCommand), reportId = request.ReportId
                                 });

        report.ResolvedBy(admin);

        await tagReportRepository.UpdateReport(report.Id, report);
        return VoidResponseDto.Executed;
    }
}
