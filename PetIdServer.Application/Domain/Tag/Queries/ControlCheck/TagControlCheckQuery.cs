using MediatR;
using PetIdServer.Application.Domain.Tag.Dto;

namespace PetIdServer.Application.Domain.Tag.Queries.ControlCheck;

public class TagControlCheckQuery : IRequest<CheckTagDto>
{
    public long ControlCode { get; set; }
}
