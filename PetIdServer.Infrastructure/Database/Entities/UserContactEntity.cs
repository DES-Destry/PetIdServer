namespace PetIdServer.Infrastructure.Database.Entities;

public class UserContactEntity
{
    public required Guid UserId { get; init; }
    public required string ContactType { get; init; }
    public required string Contact { get; init; }

    public UserEntity User { get; } = null!;
}
