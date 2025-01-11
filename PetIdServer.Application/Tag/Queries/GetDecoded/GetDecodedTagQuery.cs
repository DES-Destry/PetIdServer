using MediatR;
using PetIdServer.Application.Tag.Dto;

namespace PetIdServer.Application.Tag.Queries.GetDecoded;

public class GetDecodedTagQuery : IRequest<TagForAdminDto>
{
    public int Id { get; set; }
}
