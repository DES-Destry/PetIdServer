namespace PetIdServer.RestApi.Endpoints.Dto.Pet;

public record CreatePetDto(
    string Type,
    string Name,
    bool Sex,
    bool IsCastrated,
    string Description);
