namespace PetIdServer.Application.Tags.Dto;

public record TagForAdminDto(
    int Id,
    string PublicCode,
    string ControlCode,
    bool IsAlreadyInUse,
    DateTime CreatedAt,
    DateTime? LastScannedAt);
