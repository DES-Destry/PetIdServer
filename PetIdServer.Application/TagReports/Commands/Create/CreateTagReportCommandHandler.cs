using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Tags;
using PetIdServer.Core.Tags;
using PetIdServer.Core.Tags.Exceptions;
using PetIdServer.Core.Users;

namespace PetIdServer.Application.TagReports.Commands.Create;

public class CreateTagReportCommandHandler(
    ITagRepository tagRepository)
    : IRequestHandler<CreateTagReportCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        CreateTagReportCommand request,
        CancellationToken cancellationToken)
    {
        Tag reportedTag = await tagRepository.GetTagById((TagId)request.TagId) ??
                          throw new TagNotFoundException(new
                          {
                              Command = nameof(CreateTagReportCommand),
                              TagId = request.TagId
                          });

        reportedTag.ReportBy((UserId)request.AdminId);
        await tagRepository.UpdateTag(reportedTag);

        return VoidResponseDto.Executed;
    }
}
