using PetIdServer.Application.Users.Commands.ChangePassword;
using PetIdServer.RestApi.Binding;

namespace PetIdServer.RestApi.Endpoints.Dto.User;

public record ChangePasswordDto(string? OldPassword, string NewPassword)
{
    public ChangePasswordCommand ToCommand(RequestUser user) => new()
    {
        RequesterId = user.Id,
        OldPassword = OldPassword,
        NewPassword = NewPassword
    };
}
