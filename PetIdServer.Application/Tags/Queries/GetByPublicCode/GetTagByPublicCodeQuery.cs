using MediatR;
using PetIdServer.Application.Tags.Dto;

namespace PetIdServer.Application.Tags.Queries.GetByPublicCode;

public class GetTagByPublicCodeQuery : IRequest<TagDto>
{
    public required string Code { get; init; }
}
