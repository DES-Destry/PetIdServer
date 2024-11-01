using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.Domain.Tag.Commands.Create;

public class CreateTagCommand : IRequest<VoidResponseDto>
{
    public int Id { get; set; }

    /// <summary>
    ///     A private code
    /// </summary>
    public string Code { get; set; }
}
