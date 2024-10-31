using PetIdServer.Infrastructure.Database.Domain.Pet;

namespace PetIdServer.Infrastructure.Database.Domain.Owner;

public class OwnerModel
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
    public string? Address { get; init; }
    public string? Description { get; init; }

    public ICollection<OwnerContactModel> Contacts { get; } = [];
    public ICollection<PetModel> Pets { get; } = [];
}
