using MediatR;
using PetIdServer.Application.Tags.Dto;

namespace PetIdServer.Application.Tags.Queries.ControlCheck;

public class TagControlCheckQuery : IRequest<CheckTagDto>
{
    public required long ControlCode { get; init; }
}
