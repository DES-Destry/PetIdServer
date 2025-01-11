using MediatR;
using PetIdServer.Application.Tag.Dto;

namespace PetIdServer.Application.Tag.Queries.GetByPublicCode;

public class GetTagByPublicCodeQuery : IRequest<TagDto>
{
    public required string Code { get; set; }
}
