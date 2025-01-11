using PetIdServer.Application.User.Dto;
using PetIdServer.Application.User.Dto.Tokens;

namespace PetIdServer.Application.User.Services;

public interface IUserTokenService
{
    Task<TokenPairDto> GenerateTokens(UserDto user);
    Task<TokenPairDto> RefreshTokens(string refreshToken);

    Task<UserDto> GetUserFromToken(string accessToken);
}
