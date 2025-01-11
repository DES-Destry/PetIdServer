using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Tags;
using PetIdServer.Application.Users;
using PetIdServer.Core.TagReports;
using PetIdServer.Core.Tags;
using PetIdServer.Core.Tags.Exceptions;
using PetIdServer.Core.Users;
using PetIdServer.Core.Users.Exceptions;

namespace PetIdServer.Application.TagReports.Commands.Create;

public class CreateTagReportCommandHandler(
    IUserRepository userRepository,
    ITagRepository tagRepository,
    ITagReportRepository reportRepository)
    : IRequestHandler<CreateTagReportCommand, VoidResponseDto>
{
    public async Task<VoidResponseDto> Handle(
        CreateTagReportCommand request,
        CancellationToken cancellationToken)
    {
        User admin = await userRepository.GetUserById((UserId)request.AdminId) ??
                     throw new UserNotFoundException("Authorized admin not found!", new
                     {
                         command = nameof(CreateTagReportCommand), adminId = request.AdminId
                     });

        Tag reportedTag = await tagRepository.GetTagById((TagId)request.TagId) ??
                          throw new TagNotFoundException(new
                          {
                              command = nameof(CreateTagReportCommand), tagId = request.TagId
                          });

        TagReport.CreationAttributes creationAttributes = new(reportedTag, admin);
        TagReport report = new(creationAttributes);

        await reportRepository.CreateReport(report);

        return VoidResponseDto.Executed;
    }
}
