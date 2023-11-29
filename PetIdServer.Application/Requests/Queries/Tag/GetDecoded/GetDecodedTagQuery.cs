using MediatR;
using PetIdServer.Application.Dto.Tag;

namespace PetIdServer.Application.Requests.Queries.Tag.GetDecoded;

public class GetDecodedTagQuery : IRequest<TagForAdminDto>
{
    public int Id { get; set; }
}