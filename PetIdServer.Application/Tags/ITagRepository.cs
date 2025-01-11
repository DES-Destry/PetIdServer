using PetIdServer.Core.Pets;
using PetIdServer.Core.Tags;

namespace PetIdServer.Application.Tags;

public interface ITagRepository
{
    Task<bool> AreIdsAvailable(IEnumerable<int> ids);
    Task<bool> AreHashCodesAvailable(IEnumerable<string> codes);
    Task<IEnumerable<Tag>> GetAllTags();
    Task<Tag?> GetTagById(TagId id);
    Task<Tag?> GetTagByHashCode(string hashCode);
    Task<Tag?> GetTagByControlCode(long controlCode);

    Task<Tag?> CreateTag(Tag tag);
    Task CreateTagsBatch(IEnumerable<Tag> tags);
    Task AttachPet(TagId id, Pet pet);

    Task UpdateTag(TagId id, Tag tag);
}
