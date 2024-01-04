using AutoMapper;
using MediatR;
using PetIdServer.Application.Domain.Tag.Dto;

namespace PetIdServer.Application.Domain.Tag.Queries.GetAll;

public class GetAllTagsQueryHandler(IMapper mapper, ITagRepository tagRepository)
    : IRequestHandler<GetAllTagsQuery, TagReviewList>
{
    public async Task<TagReviewList> Handle(
        GetAllTagsQuery request,
        CancellationToken cancellationToken)
    {
        var tagsFromDb = await tagRepository.GetAllTags();
        var tags = tagsFromDb.Select(mapper.Map<Core.Domain.Tag.Tag, TagReviewForAdminDto>);

        return new TagReviewList(tags);
    }
}
