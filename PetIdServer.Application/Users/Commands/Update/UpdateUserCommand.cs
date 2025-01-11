using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.Users.Commands.Update;

public class UpdateUserCommand : IRequest<VoidResponseDto>
{
    public required Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Description { get; set; }
}
