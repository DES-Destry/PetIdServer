using MediatR;
using PetIdServer.Application.Dto;

namespace PetIdServer.Application.Requests.Commands.Owner.Update;

public class UpdateOwnerCommand : IRequest<VoidResponseDto>
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Description { get; set; }
}