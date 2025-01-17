using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.TagReports.Commands.Create;

public class CreateTagReportCommand : IRequest<VoidResponseDto>
{
    public required Guid AdminId { get; init; }
    public required int TagId { get; init; }
}
