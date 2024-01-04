using AutoMapper;
using MediatR;
using PetIdServer.Application.AppDomain.TagDomain.Dto;
using PetIdServer.Core.Domain.Tag;

namespace PetIdServer.Application.AppDomain.TagDomain.Queries.GetAll;

public class GetAllTagsQueryHandler(IMapper mapper, ITagRepository tagRepository)
    : IRequestHandler<GetAllTagsQuery, TagReviewList>
{
    public async Task<TagReviewList> Handle(
        GetAllTagsQuery request,
        CancellationToken cancellationToken)
    {
        var tagsFromDb = await tagRepository.GetAllTags();
        var tags = tagsFromDb.Select(mapper.Map<Tag, TagReviewForAdminDto>);

        return new TagReviewList(tags);
    }
}
