using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.Tags.Commands.Clear;

public class ClearTagCommand : IRequest<VoidResponseDto>
{
    public required Guid AdminId { get; init; }
    public required int TagId { get; init; }
}
