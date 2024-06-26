using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using PetIdServer.Application.Common.Exceptions;
using PetIdServer.Application.Common.Services;

namespace PetIdServer.Infrastructure.Services;

public class PasswordService(IConfiguration configuration) : IPasswordService
{
    private readonly string _salt = configuration["Security:Salt"] ??
                                    throw new MisconfigurationException().WithMeta(new
                                    {
                                        configuration,
                                        value = "Security:Salt",
                                        @class = nameof(PasswordService)
                                    });

    // Code from MS Guide: https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-7.0
    public async Task<string> HashPassword(string password)
    {
        var saltBase64 = Convert.FromBase64String(_salt);

        var hashBytes = KeyDerivation.Pbkdf2(
            password,
            saltBase64,
            KeyDerivationPrf.HMACSHA512,
            100_000,
            256 / 8);
        var hash = Convert.ToBase64String(hashBytes);

        return await Task.FromResult(hash);
    }

    public async Task<bool> ValidatePassword(string password, string hash)
    {
        var newPasswordHash = await HashPassword(password);
        return hash == newPasswordHash;
    }
}
