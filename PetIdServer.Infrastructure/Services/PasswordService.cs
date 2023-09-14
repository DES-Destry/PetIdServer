using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using PetIdServer.Application.Services;

namespace PetIdServer.Infrastructure.Services;

public class PasswordService : IPasswordService
{
    private readonly string _salt;

    public PasswordService(IConfiguration configuration)
    {
        _salt = configuration["Security:Salt"] ?? throw new ArgumentException(nameof(configuration));
    }

    // Code from MS Guide: https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-7.0
    public async Task<string> HashPassword(string password)
    {
        var saltBase64 = Convert.FromBase64String(_salt);

        var hashBytes = KeyDerivation.Pbkdf2(
            password: password, 
            salt: saltBase64, 
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 100_000, 
            numBytesRequested: 256 / 8);
        var hash = Convert.ToBase64String(hashBytes);

        return await Task.FromResult(hash);
    }

    public async Task<bool> ValidatePassword(string password, string hash)
    {
        var newPasswordHash = await HashPassword(password);
        return hash == newPasswordHash;
    }
}