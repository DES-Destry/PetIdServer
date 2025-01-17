using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Users;
using PetIdServer.Core.TagReports;
using PetIdServer.Core.TagReports.Exceptions;
using PetIdServer.Core.Users;
using PetIdServer.Core.Users.Exceptions;

namespace PetIdServer.Application.TagReports.Commands.Resolve;

public class ResolveTagReportCommandHandler(
    IUserRepository userRepository,
    ITagReportRepository tagReportRepository)
    : IRequestHandler<ResolveTagReportCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        ResolveTagReportCommand request,
        CancellationToken cancellationToken)
    {
        User admin = await userRepository.GetUserById((UserId)request.AdminId) ??
                     throw new UserNotFoundException("Authorized admin not found", new
                     {
                         command = nameof(ResolveTagReportCommand), adminId = request.AdminId
                     });

        TagReport report = await tagReportRepository.GetTagReportById((TagReportId)request.ReportId) ??
                           throw new TagReportNotFoundException(new
                           {
                               command = nameof(ResolveTagReportCommand), reportId = request.ReportId
                           });

        report.ResolvedBy(admin);

        // TODO null will be removed with correct logic of AggregateRoot / Entity segregation
        await tagReportRepository.UpdateReport(report, null!);
        return VoidResponseDto.Executed;
    }
}
