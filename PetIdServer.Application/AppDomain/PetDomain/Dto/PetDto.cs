namespace PetIdServer.Application.AppDomain.PetDomain.Dto;

public record PetDto(
    string Type,
    string Name,
    bool Sex,
    bool IsCastrated,
    string? Photo,
    string? Description
);
