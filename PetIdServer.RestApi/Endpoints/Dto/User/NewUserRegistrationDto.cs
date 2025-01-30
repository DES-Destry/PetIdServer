using PetIdServer.Application.Users.Commands.Registration;

namespace PetIdServer.RestApi.Endpoints.Dto.User;

public record NewUserRegistrationDto(string Email, string Password, string Name)
{
    public RegistrationCommand ToCommand() => new()
    {
        Email = Email,
        Password = Password,
        Name = Name
    };
}
