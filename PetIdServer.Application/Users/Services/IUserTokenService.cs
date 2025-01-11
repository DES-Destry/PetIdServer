using PetIdServer.Application.Users.Dto;
using PetIdServer.Application.Users.Dto.Tokens;

namespace PetIdServer.Application.Users.Services;

public interface IUserTokenService
{
    Task<TokenPairDto> GenerateTokens(UserDto user);
    Task<TokenPairDto> RefreshTokens(string refreshToken);

    Task<UserDto> GetUserFromToken(string accessToken);
}
