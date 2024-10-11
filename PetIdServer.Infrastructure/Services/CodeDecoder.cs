using System.Security.Cryptography;
using System.Text;
using PetIdServer.Application.Common.Services;

namespace PetIdServer.Infrastructure.Services;

public class CodeDecoder : ICodeDecoder
{
    private readonly string _privateKey = File.ReadAllText(Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory + "../../../", "Keys", "private.pem"));
    // private readonly RSAParameters _privateRsaParameters;

    private readonly string _publicKey = File.ReadAllText(Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory + "../../../", "Keys", "public.pem"));
    // private readonly RSAParameters _publicRsaParameters;

    // _publicRsaParameters = ExtractPublicRsaParameters(_publicKey);
    // _privateRsaParameters = ExtractPrivateRsaParameters(_privateKey);

    public async Task<string> EncodePublicCode(string publicCode)
    {
        var publicBytes = Encoding.UTF8.GetBytes(publicCode);

        using var rsa = RSA.Create();
        rsa.ImportFromPem(_publicKey);

        var encryptedBytes = rsa.Encrypt(publicBytes, RSAEncryptionPadding.Pkcs1);
        return await Task.FromResult(Convert.ToBase64String(encryptedBytes));
    }

    public async Task<string> GetPublicCodeOriginal(string privateCode)
    {
        var privateBytes = Convert.FromBase64String(privateCode);

        using var rsa = RSA.Create();
        rsa.ImportFromPem(_privateKey);

        var decryptedBytes = rsa.Decrypt(privateBytes, RSAEncryptionPadding.Pkcs1);
        return await Task.FromResult(Encoding.UTF8.GetString(decryptedBytes));
    }

    // private async Task<string> Execute(Action action, string code)
    // {
    //     using var publicProvider = RSA.Create();
    //     using var privateProvider = RSA.Create();
    //
    //     publicProvider.ImportParameters(_publicRsaParameters);
    //     privateProvider.ImportParameters(_privateRsaParameters);
    //
    //     var resultCodeBytes = action switch
    //     {
    //         Action.Decrypt => privateProvider.Decrypt(Convert.FromBase64String(code),
    //             RSAEncryptionPadding.OaepSHA256),
    //         Action.Encrypt => privateProvider.Encrypt(Convert.FromHexString(code),
    //             RSAEncryptionPadding.OaepSHA256),
    //         _ => []
    //     };
    //
    //     var resultCode = action switch
    //     {
    //         Action.Decrypt => Encoding.UTF8.GetString(resultCodeBytes),
    //         Action.Encrypt => Convert.ToBase64String(resultCodeBytes),
    //         _ => string.Empty
    //     };
    //
    //     return await Task.FromResult(resultCode);
    // }

    // private static RSAParameters ExtractPublicRsaParameters(string publicKey)
    // {
    //     RSACryptoServiceProvider rsa = new();
    //     rsa.ImportFromPem(publicKey);
    //     return rsa.ExportParameters(false);
    // }
    //
    // private static RSAParameters ExtractPrivateRsaParameters(string privateKey)
    // {
    //     RSACryptoServiceProvider rsa = new();
    //     rsa.ImportFromPem(privateKey);
    //     return rsa.ExportParameters(true);
    // }
    //
    // private enum Action { Decrypt, Encrypt }
}
