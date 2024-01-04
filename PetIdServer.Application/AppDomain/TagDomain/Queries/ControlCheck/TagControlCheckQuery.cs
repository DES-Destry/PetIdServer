using MediatR;
using PetIdServer.Application.AppDomain.TagDomain.Dto;

namespace PetIdServer.Application.AppDomain.TagDomain.Queries.ControlCheck;

public class TagControlCheckQuery : IRequest<CheckTagDto>
{
    public long ControlCode { get; set; }
}
