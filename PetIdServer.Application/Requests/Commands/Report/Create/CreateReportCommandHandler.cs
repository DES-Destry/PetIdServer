using MediatR;
using PetIdServer.Application.Dto;
using PetIdServer.Application.Repositories;
using PetIdServer.Core.Domains.Admin.Exceptions;
using PetIdServer.Core.Domains.Tag.Exceptions;

namespace PetIdServer.Application.Requests.Commands.Report.Create;

public class CreateReportCommandHandler(
    IAdminRepository adminRepository,
    ITagRepository tagRepository,
    IReportRepository reportRepository) : IRequestHandler<CreateReportCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        CreateReportCommand request,
        CancellationToken cancellationToken)
    {
        var admin = await adminRepository.GetAdminById(request.AdminId);

        if (admin is null)
            throw new AdminNotFoundException("Authorized admin not found!", new
            {
                command = nameof(CreateReportCommand),
                adminId = request.AdminId.Value
            });

        var reportedTag = await tagRepository.GetTagById(request.TagId);

        if (reportedTag is null)
            throw new TagNotFoundException(new
            {
                command = nameof(CreateReportCommand),
                tagId = request.TagId.Value
            });

        var report =
            new Core.Domains.Report.Report(
                new Core.Domains.Report.Report.CreationAttributes(reportedTag, admin));

        await reportRepository.CreateReport(report);

        return VoidResponseDto.Executed;
    }
}
