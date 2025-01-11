using MediatR;
using PetIdServer.Application.Tag.Dto;

namespace PetIdServer.Application.Tag.Queries.GetAll;

public class GetAllTagsQuery : IRequest<TagReviewList>
{
}
