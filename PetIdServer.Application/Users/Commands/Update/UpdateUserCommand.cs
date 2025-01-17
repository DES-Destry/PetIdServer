using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.Users.Commands.Update;

public class UpdateUserCommand : IRequest<VoidResponseDto>
{
    public required Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Address { get; init; }
    public string? Description { get; init; }
}
