using MediatR;
using PetIdServer.Application.Dto.Tag;

namespace PetIdServer.Application.Requests.Queries.Tag.GetAll;

public class GetAllTagsQuery : IRequest<TagReviewList>
{
}
