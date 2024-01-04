using MediatR;
using PetIdServer.Application.Dto.Tag;

namespace PetIdServer.Application.Requests.Queries.Tag.ControlCheck;

public class TagControlCheckQuery : IRequest<CheckTagDto>
{
    public long ControlCode { get; set; }
}
