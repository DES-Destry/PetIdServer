using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Domain.Admin;
using PetIdServer.Application.Domain.Tag;
using PetIdServer.Core.Domain.Admin.Exceptions;
using PetIdServer.Core.Domain.Tag.Exceptions;

namespace PetIdServer.Application.Domain.Report.Commands.Create;

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
            new Core.Domain.Report.Report(
                new Core.Domain.Report.Report.CreationAttributes(reportedTag, admin));

        await reportRepository.CreateReport(report);

        return VoidResponseDto.Executed;
    }
}
