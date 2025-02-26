namespace PetIdServer.Persistence.Entities;

public class UserEntity
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Role { get; init; }
    public string? Password { get; init; }
    public string? Address { get; init; }
    public string? Description { get; init; }

    public ICollection<UserContactEntity> Contacts { get; init; } = [];
    public ICollection<PetEntity> Pets { get; init; } = [];

    public ICollection<TagReportEntity> TagReportsCreated { get; init; } = [];
    public ICollection<TagReportEntity> TagReportsResolved { get; init; } = [];
    public ICollection<TagHistoryEntryEntity> TagActions { get; init; } = [];
}
