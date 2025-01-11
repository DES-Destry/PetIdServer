using MediatR;
using PetIdServer.Application.Common.Dto;

namespace PetIdServer.Application.Tag.Commands.CreateBatch;

public class CreateTagsBatchCommand : IRequest<VoidResponseDto>
{
    public int IdFrom { get; set; }
    public int IdTo { get; set; }
    public IEnumerable<string> Codes { get; set; }
}
