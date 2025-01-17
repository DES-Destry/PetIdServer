using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.TagReports.Dto;
using PetIdServer.Application.Tags;
using PetIdServer.Core.TagReports;
using PetIdServer.Core.TagReports.Exceptions;
using PetIdServer.Core.Tags;
using PetIdServer.Core.Tags.Exceptions;
using PetIdServer.Core.Users;

namespace PetIdServer.Application.TagReports.Commands.Resolve;

public class ResolveTagReportCommandHandler(
    ITagRepository tagRepository,
    ITagReportRepository tagReportRepository)
    : IRequestHandler<ResolveTagReportCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        ResolveTagReportCommand request,
        CancellationToken cancellationToken)
    {
        TagReportDto report = await tagReportRepository.GetTagReportById((TagReportId)request.ReportId) ??
                              throw new TagReportNotFoundException(new
                              {
                                  command = nameof(ResolveTagReportCommand), reportId = request.ReportId
                              });

        Tag tag = await tagRepository.GetTagById(report.CorruptedTagId) ??
                  throw new TagNotFoundException("Report's paired tag wasn't found", new
                  {
                      Command = nameof(ResolveTagReportCommand), TagId = report.CorruptedTagId, ReportId = report.Id
                  });

        tag.ResolveReportBy(report.Id, (UserId)request.AdminId);

        return VoidResponseDto.Executed;
    }
}
