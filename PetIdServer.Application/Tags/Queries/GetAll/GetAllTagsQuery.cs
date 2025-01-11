using MediatR;
using PetIdServer.Application.Tags.Dto;

namespace PetIdServer.Application.Tags.Queries.GetAll;

public class GetAllTagsQuery : IRequest<TagReviewList>;
