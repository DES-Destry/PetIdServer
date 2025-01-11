using System.Security.Cryptography;
using System.Text;
using PetIdServer.Application.Tags.Services;

namespace PetIdServer.Infrastructure.Services;

public class CodeDecoder : ICodeDecoder
{
    private readonly string _privateKey = File.ReadAllText(Path.Combine(
                                                               AppDomain.CurrentDomain.BaseDirectory + "../../../", "Keys",
                                                               "private.pem"));

    private readonly string _publicKey = File.ReadAllText(Path.Combine(
                                                              AppDomain.CurrentDomain.BaseDirectory + "../../../", "Keys",
                                                              "public.pem"));

    public async Task<string> EncodePublicCode(string publicCode)
    {
        byte[] publicBytes = Encoding.UTF8.GetBytes(publicCode);

        using RSA rsa = RSA.Create();
        rsa.ImportFromPem(_publicKey);

        byte[] encryptedBytes = rsa.Encrypt(publicBytes, RSAEncryptionPadding.OaepSHA256);
        return await Task.FromResult(Convert.ToBase64String(encryptedBytes));
    }

    public async Task<string> GetPublicCodeOriginal(string privateCode)
    {
        byte[] privateBytes = Convert.FromBase64String(privateCode);

        using RSA rsa = RSA.Create();
        rsa.ImportFromPem(_privateKey);

        byte[] decryptedBytes = rsa.Decrypt(privateBytes, RSAEncryptionPadding.OaepSHA256);
        return await Task.FromResult(Encoding.UTF8.GetString(decryptedBytes));
    }
}
