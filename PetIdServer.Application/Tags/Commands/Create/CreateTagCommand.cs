using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.Tags.Commands.Create;

public class CreateTagCommand : IRequest<VoidResponseDto>
{
    public required int Id { get; init; }

    /// <summary>
    ///     A private code
    /// </summary>
    public required string Code { get; init; }
}
