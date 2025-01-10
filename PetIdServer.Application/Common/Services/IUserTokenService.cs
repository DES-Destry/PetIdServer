using PetIdServer.Application.Common.Dto;
using PetIdServer.Application.Common.Services.Dto;

namespace PetIdServer.Application.Common.Services;

public interface IUserTokenService
{
    Task<TokenPairDto> GenerateTokens(UserDto user);
    Task<TokenPairDto> RefreshTokens(string refreshToken);

    Task<UserDto> GetUserFromToken(string accessToken);
}
