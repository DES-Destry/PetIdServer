using AutoMapper;
using MediatR;
using PetIdServer.Application.Tags.Dto;
using PetIdServer.Core.Tags;

namespace PetIdServer.Application.Tags.Queries.GetAll;

public class GetAllTagsQueryHandler(IMapper mapper, ITagRepository tagRepository)
    : IRequestHandler<GetAllTagsQuery, TagReviewList>
{
    public async Task<TagReviewList> Handle(
        GetAllTagsQuery request,
        CancellationToken cancellationToken)
    {
        IEnumerable<Tag>? tagsFromDb = await tagRepository.GetAllTags();
        IEnumerable<TagReviewForAdminDto>? tags = tagsFromDb.Select(mapper.Map<Tag, TagReviewForAdminDto>);

        return new TagReviewList(tags);
    }
}
