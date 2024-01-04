using MediatR;
using PetIdServer.Application.AppDomain.TagDomain.Dto;

namespace PetIdServer.Application.AppDomain.TagDomain.Queries.GetAll;

public class GetAllTagsQuery : IRequest<TagReviewList>
{
}
