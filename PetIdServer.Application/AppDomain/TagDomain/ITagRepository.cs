using PetIdServer.Core.Domain.Pet;
using PetIdServer.Core.Domain.Tag;

namespace PetIdServer.Application.AppDomain.TagDomain;

public interface ITagRepository
{
    Task<bool> IsIdsAvailable(IEnumerable<int> ids);
    Task<bool> IsCodesAvailable(IEnumerable<string> codes);
    Task<IEnumerable<TagEntity>> GetAllTags();
    Task<TagEntity?> GetTagById(TagId id);
    Task<TagEntity?> GetByCode(string code);
    Task<TagEntity?> GetByControlCode(long controlCode);

    Task<TagEntity?> CreateTag(TagEntity tag);
    Task CreateTagsBatch(IEnumerable<TagEntity> tags);
    Task AttachPet(TagId id, PetEntity pet);

    Task UpdateTag(TagId id, TagEntity tag);
}
