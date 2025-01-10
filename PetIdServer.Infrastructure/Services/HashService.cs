using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using PetIdServer.Application.Common.Services;
using PetIdServer.Core.Domain.User;

namespace PetIdServer.Infrastructure.Services;

public class HashService : IHashService
{
    private const int SaltSize = 0x10;
    private const int HashSize = 0x20;
    private const int Iterations = 100_000;
    private const KeyDerivationPrf HashAlgorithm = KeyDerivationPrf.HMACSHA512;

    public async Task<PasswordHash> Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = KeyDerivation.Pbkdf2(password, salt, HashAlgorithm, Iterations, HashSize);

        PasswordHash passwordHash = PasswordHash.FromHash($"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}");
        return await Task.FromResult(passwordHash);
    }

    public async Task<bool> Validate(string value, string valueHash)
    {
        string[] parts = valueHash.Split('-');

        byte[] hash = Convert.FromHexString(parts[0]);
        byte[] salt = Convert.FromHexString(parts[1]);

        byte[] inputHash = KeyDerivation.Pbkdf2(value, salt, HashAlgorithm, Iterations, HashSize);

        bool result = CryptographicOperations.FixedTimeEquals(hash, inputHash);
        return await Task.FromResult(result);
    }
}
