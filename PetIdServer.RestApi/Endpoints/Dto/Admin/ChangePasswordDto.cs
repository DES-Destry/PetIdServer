namespace PetIdServer.RestApi.Endpoints.Dto.Admin;

public record ChangePasswordDto(string OldPassword, string NewPassword);
