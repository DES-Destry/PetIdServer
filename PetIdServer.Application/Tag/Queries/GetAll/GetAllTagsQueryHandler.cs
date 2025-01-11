using AutoMapper;
using MediatR;
using PetIdServer.Application.Tag.Dto;
using PetIdServer.Core.Domain.Tag;

namespace PetIdServer.Application.Tag.Queries.GetAll;

public class GetAllTagsQueryHandler(IMapper mapper, ITagRepository tagRepository)
    : IRequestHandler<GetAllTagsQuery, TagReviewList>
{
    public async Task<TagReviewList> Handle(
        GetAllTagsQuery request,
        CancellationToken cancellationToken)
    {
        IEnumerable<TagEntity>? tagsFromDb = await tagRepository.GetAllTags();
        IEnumerable<TagReviewForAdminDto>? tags = tagsFromDb.Select(mapper.Map<TagEntity, TagReviewForAdminDto>);

        return new TagReviewList(tags);
    }
}
