using MediatR;
using PetIdServer.Application.Domain.Tag.Dto;

namespace PetIdServer.Application.Domain.Tag.Queries.GetAll;

public class GetAllTagsQuery : IRequest<TagReviewList>
{
}
