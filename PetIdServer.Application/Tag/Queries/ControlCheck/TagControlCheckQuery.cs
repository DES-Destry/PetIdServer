using MediatR;
using PetIdServer.Application.Tag.Dto;

namespace PetIdServer.Application.Tag.Queries.ControlCheck;

public class TagControlCheckQuery : IRequest<CheckTagDto>
{
    public required long ControlCode { get; init; }
}
