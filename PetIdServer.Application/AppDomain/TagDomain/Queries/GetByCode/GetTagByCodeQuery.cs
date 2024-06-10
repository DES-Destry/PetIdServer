using MediatR;
using PetIdServer.Application.AppDomain.TagDomain.Dto;

namespace PetIdServer.Application.AppDomain.TagDomain.Queries.GetByCode;

public class GetTagByCodeQuery : IRequest<CheckTagDto>
{
    public string Code { get; set; }
}
