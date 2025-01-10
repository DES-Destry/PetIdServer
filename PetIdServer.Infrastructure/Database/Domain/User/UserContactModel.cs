namespace PetIdServer.Infrastructure.Database.Domain.User;

public class UserContactModel
{
    public required Guid UserId { get; init; }
    public required string ContactType { get; init; }
    public required string Contact { get; init; }

    public UserModel User { get; } = null!;
}
