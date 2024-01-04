using PetIdServer.Application.Dto;
using PetIdServer.Application.Services.Dto;

namespace PetIdServer.Application.Services;

public interface IOwnerTokenService
{
    Task<TokenPairDto> GenerateTokens(OwnerDto owner);
    Task<TokenPairDto> RefreshTokens(string refreshToken);

    Task<OwnerDto> DecryptOwner(string accessToken);
}
