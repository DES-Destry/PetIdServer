using MediatR;
using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Tag;
using PetIdServer.Application.User;
using PetIdServer.Core.Domain.Tag;
using PetIdServer.Core.Domain.Tag.Exceptions;
using PetIdServer.Core.Domain.TagReport;
using PetIdServer.Core.Domain.User;
using PetIdServer.Core.Domain.User.Exceptions;

namespace PetIdServer.Application.TagReport.Commands.Create;

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
        UserEntity admin = await userRepository.GetUserById((UserId)request.AdminId) ??
                           throw new UserNotFoundException("Authorized admin not found!", new
                           {
                               command = nameof(CreateTagReportCommand), adminId = request.AdminId
                           });

        TagEntity reportedTag = await tagRepository.GetTagById((TagId)request.TagId) ??
                                throw new TagNotFoundException(new
                                {
                                    command = nameof(CreateTagReportCommand), tagId = request.TagId
                                });

        TagReportEntity.CreationAttributes creationAttributes = new(reportedTag, admin);
        TagReportEntity report = new(creationAttributes);

        await reportRepository.CreateReport(report);

        return VoidResponseDto.Executed;
    }
}
