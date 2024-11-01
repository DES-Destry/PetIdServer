using MediatR;
using PetIdServer.Application.Domain.Tag.Dto;

namespace PetIdServer.Application.Domain.Tag.Queries.GetDecoded;

public class GetDecodedTagQuery : IRequest<TagForAdminDto>
{
    public int Id { get; set; }
}
