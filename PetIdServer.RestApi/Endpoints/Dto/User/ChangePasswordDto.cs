namespace PetIdServer.RestApi.Endpoints.Dto.User;

public record ChangePasswordDto(string? OldPassword, string NewPassword);
