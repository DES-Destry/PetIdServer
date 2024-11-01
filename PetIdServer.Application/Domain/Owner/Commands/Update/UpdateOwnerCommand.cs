using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.Domain.Owner.Commands.Update;

public class UpdateOwnerCommand : IRequest<VoidResponseDto>
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Description { get; set; }
}
