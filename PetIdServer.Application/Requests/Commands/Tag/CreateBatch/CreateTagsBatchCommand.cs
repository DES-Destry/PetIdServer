using MediatR;
using PetIdServer.Application.Dto;

namespace PetIdServer.Application.Requests.Commands.Tag.CreateBatch;

public class CreateTagsBatchCommand : IRequest<VoidResponseDto>
{
    public int IdFrom { get; set; }
    public int IdTo { get; set; }
    public IEnumerable<string> Codes { get; set; }
}
