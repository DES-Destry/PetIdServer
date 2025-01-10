using PetIdServer.Infrastructure.Database.Domain.Pet;
using PetIdServer.Infrastructure.Database.Domain.Tag;

namespace PetIdServer.Infrastructure.Database.Domain.User;

public class UserModel
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Role { get; init; }
    public string? Password { get; init; }
    public string? Address { get; init; }
    public string? Description { get; init; }

    public ICollection<UserContactModel> Contacts { get; } = [];
    public ICollection<PetModel> Pets { get; } = [];

    public ICollection<TagReportModel> TagReportsCreated { get; } = [];
    public ICollection<TagReportModel> TagReportsResolved { get; } = [];
}
