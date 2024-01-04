using PetIdServer.Core.Domain.Tag;

namespace PetIdServer.Application.Domain.Tag;

public interface ITagRepository
{
    Task<bool> IsIdsAvailable(IEnumerable<int> ids);
    Task<bool> IsCodesAvailable(IEnumerable<string> codes);
    Task<IEnumerable<Core.Domain.Tag.Tag>> GetAllTags();
    Task<Core.Domain.Tag.Tag?> GetTagById(TagId id);
    Task<Core.Domain.Tag.Tag?> GetByCode(string code);
    Task<Core.Domain.Tag.Tag?> GetByControlCode(long controlCode);

    Task<Core.Domain.Tag.Tag?> CreateTag(Core.Domain.Tag.Tag tag);
    Task CreateTagsBatch(IEnumerable<Core.Domain.Tag.Tag> tags);
    Task AttachPet(TagId id, Core.Domain.Pet.Pet pet);
}
