namespace PetIdServer.Infrastructure.Database.Entities;

public class PetModel
{
    public required Guid Id { get; init; }
    public required Guid UserId { get; init; }
    public required string Type { get; init; }
    public required string Name { get; init; }
    public required bool Sex { get; init; }
    public required bool IsCastrated { get; init; }
    public required Guid PhotoId { get; init; }
    public string? Description { get; set; }


    public UserModel User { get; } = null!;
    public ICollection<TagModel> Tags { get; } = [];
}
