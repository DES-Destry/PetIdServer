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

    public async Task<string> EncodePublicCode(string publicCode) =>
        await Execute(Action.Encrypt, publicCode);

    public async Task<string> GetPublicCodeOriginal(string privateCode) =>
        await Execute(Action.Decrypt, privateCode);

    private async Task<string> Execute(Action action, string code)
    {
        using var rsaProvider = RSA.Create();
        rsaProvider.ImportParameters(_rsaParameters);

        var inputCodeBytes = Convert.FromBase64String(code);

        var resultCodeBytes = action switch
        {
            Action.Decrypt => rsaProvider.Decrypt(inputCodeBytes, RSAEncryptionPadding.OaepSHA256),
            Action.Encrypt => rsaProvider.Encrypt(inputCodeBytes, RSAEncryptionPadding.OaepSHA256),
            _ => Array.Empty<byte>()
        };

        var resultCode = Encoding.UTF8.GetString(resultCodeBytes);
        return await Task.FromResult(resultCode);
    }

    private static RSAParameters ExtractRsaParameters(string privateKey)
    {
        var rsa = new RSACryptoServiceProvider();
        rsa.ImportFromPem(privateKey);
        return rsa.ExportParameters(true);
    }

    private enum Action { Decrypt, Encrypt }
}
