using MediatR;
using PetIdServer.Application.AppDomain.TagDomain.Dto;

namespace PetIdServer.Application.AppDomain.TagDomain.Queries.GetByPublicCode;

public class GetTagByPublicCodeQuery : IRequest<TagDto>
{
    public required string Code { get; set; }
}
