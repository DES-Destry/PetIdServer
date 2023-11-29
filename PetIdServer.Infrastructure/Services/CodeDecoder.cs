using System.Security.Cryptography;
using System.Text;
using PetIdServer.Application.Services;

namespace PetIdServer.Infrastructure.Services;

public class CodeDecoder : ICodeDecoder
{
    private readonly RSAParameters _rsaParameters;

    public CodeDecoder()
    {
        var privateKey = File.ReadAllText("./Keys/private.pem");
        _rsaParameters = ExtractRsaParameters(privateKey);
    }

    public async Task<string> EncodePublicCode(string publicCode)
    {
        return await Execute(Action.Encrypt, publicCode);
    }

    public async Task<string> GetPublicCodeOriginal(string privateCode)
    {
        return await Execute(Action.Decrypt, privateCode);
    }

    private async Task<string> Execute(Action action, string code)
    {
        using var rsaProvider = RSA.Create();
        rsaProvider.ImportParameters(_rsaParameters);

        var inputCodeBytes = Convert.FromBase64String(code);

        var resultCodeBytes = action switch
        {
            Action.Decrypt => rsaProvider.Decrypt(inputCodeBytes, RSAEncryptionPadding.Pkcs1),
            Action.Encrypt => rsaProvider.Encrypt(inputCodeBytes, RSAEncryptionPadding.Pkcs1),
            _ => Array.Empty<byte>()
        };

        var resultCode = Encoding.UTF8.GetString(resultCodeBytes);
        return await Task.FromResult(resultCode);
    }

    private static RSAParameters ExtractRsaParameters(string privateKey)
    {
        var base64PrivateKey = ExtractBase64Key(privateKey);
        var privateKeyBits = Convert.FromBase64String(base64PrivateKey);
        var rsa = new RSACryptoServiceProvider();
        rsa.ImportPkcs8PrivateKey(privateKeyBits, out _);
        return rsa.ExportParameters(true);
    }

    private static string ExtractBase64Key(string keyWithHeaders)
    {
        const string header = "-----BEGIN PRIVATE KEY-----";
        const string footer = "-----END PRIVATE KEY-----";

        return keyWithHeaders.Replace(header, "").Replace(footer, "");
    }

    private enum Action
    {
        Decrypt,
        Encrypt
    }
}