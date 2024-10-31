using PetIdServer.Infrastructure.Database.Domain.Owner;
using PetIdServer.Infrastructure.Database.Domain.Tag;

namespace PetIdServer.Infrastructure.Database.Domain.Pet;

public class PetModel
{
    public required Guid Id { get; init; }
    public required Guid OwnerId { get; init; }
    public required string Type { get; init; }
    public required string Name { get; init; }
    public required bool Sex { get; init; }
    public required bool IsCastrated { get; init; }
    public required Guid PhotoId { get; init; }
    public string? Description { get; set; }


    public OwnerModel Owner { get; } = null!;
    public ICollection<TagModel> Tags { get; } = [];
}
