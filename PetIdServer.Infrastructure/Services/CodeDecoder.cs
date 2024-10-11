using System.Security.Cryptography;
using System.Text;
using PetIdServer.Application.Common.Services;

namespace PetIdServer.Infrastructure.Services;

public class CodeDecoder : ICodeDecoder
{
    private readonly string _privateKey = File.ReadAllText(Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory + "../../../", "Keys", "private.pem"));

    private readonly string _publicKey = File.ReadAllText(Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory + "../../../", "Keys", "public.pem"));

    public async Task<string> EncodePublicCode(string publicCode)
    {
        var publicBytes = Encoding.UTF8.GetBytes(publicCode);

        using var rsa = RSA.Create();
        rsa.ImportFromPem(_publicKey);

        var encryptedBytes = rsa.Encrypt(publicBytes, RSAEncryptionPadding.OaepSHA256);
        return await Task.FromResult(Convert.ToBase64String(encryptedBytes));
    }

    public async Task<string> GetPublicCodeOriginal(string privateCode)
    {
        var privateBytes = Convert.FromBase64String(privateCode);

        using var rsa = RSA.Create();
        rsa.ImportFromPem(_privateKey);

        var decryptedBytes = rsa.Decrypt(privateBytes, RSAEncryptionPadding.OaepSHA256);
        return await Task.FromResult(Encoding.UTF8.GetString(decryptedBytes));
    }
}
