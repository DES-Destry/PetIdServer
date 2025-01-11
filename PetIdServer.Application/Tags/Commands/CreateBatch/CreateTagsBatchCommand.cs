using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.Tags.Commands.CreateBatch;

public class CreateTagsBatchCommand : IRequest<VoidResponseDto>
{
    public required int IdFrom { get; init; }
    public required int IdTo { get; init; }
    public required IEnumerable<string> Codes { get; init; }
}
