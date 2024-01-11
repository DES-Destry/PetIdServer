using MediatR;
using PetIdServer.Application.AppDomain.TagDomain.Dto;

namespace PetIdServer.Application.AppDomain.TagDomain.Queries.GetDecoded;

public class GetDecodedTagQuery : IRequest<TagForAdminDto>
{
    public int Id { get; set; }
}
