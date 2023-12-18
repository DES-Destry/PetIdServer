using AutoMapper;
using MediatR;
using PetIdServer.Application.Dto.Tag;
using PetIdServer.Application.Repositories;

namespace PetIdServer.Application.Requests.Queries.Tag.GetAll;

public class GetAllTagsQueryHandler
    (IMapper mapper, ITagRepository tagRepository) : IRequestHandler<GetAllTagsQuery, TagReviewList>
{
    public async Task<TagReviewList> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var tagsFromDb = await tagRepository.GetAllTags();
        var tags = tagsFromDb.Select(mapper.Map<Core.Entities.Tag, TagReviewForAdminDto>);

        return new TagReviewList {Tags = tags};
    }
}