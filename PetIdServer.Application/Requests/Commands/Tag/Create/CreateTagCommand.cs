using MediatR;
using PetIdServer.Application.Dto;

namespace PetIdServer.Application.Requests.Commands.Tag.Create;

public class CreateTagCommand : IRequest<VoidResponseDto>
{
    public int Id { get; set; }
    
    /// <summary>
    /// A private code
    /// </summary>
    public string Code { get; set; }
}