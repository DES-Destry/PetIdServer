using MediatR;
using PetIdServer.Application.Domain.Tag.Dto;

namespace PetIdServer.Application.Domain.Tag.Queries.GetByPublicCode;

public class GetTagByPublicCodeQuery : IRequest<TagDto>
{
    public required string Code { get; set; }
}
