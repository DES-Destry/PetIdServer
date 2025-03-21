using PetIdServer.Application.Users.Dto;
using PetIdServer.Application.Users.Dto.Tokens;
using PetIdServer.Core.Users;

namespace PetIdServer.Application.Users.Services;

public interface IUserTokenService
{
    Task<TokenPairDto> GenerateTokens(UserDto user, UserRole? withPermissionsOf = null);
    Task<TokenPairDto> RefreshTokens(string refreshToken);

    Task<UserDto> GetUserFromToken(string accessToken);
    Task<UserRole> GetPermissionsFromToken(string accessToken);
}
