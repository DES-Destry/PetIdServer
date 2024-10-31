using PetIdServer.Infrastructure.Database.Domain.Tag;

namespace PetIdServer.Infrastructure.Database.Domain.Admin;

public class AdminModel
{
    public required string Username { get; init; }
    public string? Password { get; init; }
    public required DateTime CreatedAt { get; init; }
    public DateTime? PasswordLastChangedAt { get; init; }

    public ICollection<TagReportModel> TagReportsCreated { get; } = [];
    public ICollection<TagReportModel> TagReportsResolved { get; } = [];
}
