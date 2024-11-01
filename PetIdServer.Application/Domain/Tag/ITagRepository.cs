using PetIdServer.Core.Domain.Pet;
using PetIdServer.Core.Domain.Tag;

namespace PetIdServer.Application.Domain.Tag;

public interface ITagRepository
{
    Task<bool> AreIdsAvailable(IEnumerable<int> ids);
    Task<bool> AreHashCodesAvailable(IEnumerable<string> codes);
    Task<IEnumerable<TagEntity>> GetAllTags();
    Task<TagEntity?> GetTagById(TagId id);
    Task<TagEntity?> GetTagByHashCode(string hashCode);
    Task<TagEntity?> GetTagByControlCode(long controlCode);

    Task<TagEntity?> CreateTag(TagEntity tag);
    Task CreateTagsBatch(IEnumerable<TagEntity> tags);
    Task AttachPet(TagId id, PetEntity pet);

    Task UpdateTag(TagId id, TagEntity tag);
}
