using MediatR;
using PetIdServer.Application.Tags.Dto;

namespace PetIdServer.Application.Tags.Queries.GetDecoded;

public class GetDecodedTagQuery : IRequest<TagForAdminDto>
{
    public required int Id { get; init; }
}
