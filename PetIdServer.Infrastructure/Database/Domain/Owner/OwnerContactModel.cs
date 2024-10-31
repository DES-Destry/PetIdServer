namespace PetIdServer.Infrastructure.Database.Domain.Owner;

public class OwnerContactModel
{
    public required Guid OwnerId { get; init; }
    public required string ContactType { get; init; }
    public required string Contact { get; init; }

    public OwnerModel Owner { get; } = null!;
}
