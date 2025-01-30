using PetIdServer.Application.Users.Commands.Login;

namespace PetIdServer.RestApi.Endpoints.Dto.User;

public record LoginUserDto(string Email, string Password, string? WithPermissionsOf)
{
    public LoginCommand ToCommand() => new()
    {
        Email = Email,
        Password = Password,
        WithPermissionsOf = WithPermissionsOf
    };
}
