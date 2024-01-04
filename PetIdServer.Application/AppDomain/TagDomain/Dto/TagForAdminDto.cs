namespace PetIdServer.Application.AppDomain.TagDomain.Dto;

public record TagForAdminDto(
    int Id,
    string PublicCode,
    string ControlCode,
    bool IsAlreadyInUse,
    DateTime CreatedAt,
    DateTime? PetAddedAt,
    DateTime? LastScannedAt);
