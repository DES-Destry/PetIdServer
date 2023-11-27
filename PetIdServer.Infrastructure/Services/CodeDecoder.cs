using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.OpenSsl;
using PetIdServer.Application.Services;

namespace PetIdServer.Infrastructure.Services;

public class CodeDecoder : ICodeDecoder
{
    private readonly AsymmetricKeyParameter _privateCrt;

    private readonly AsymmetricKeyParameter _publicCrt;

    public CodeDecoder()
    {
        var privateKey = File.ReadAllText("./Keys/private_key.pem");

        var pemReader = new PemReader(new StringReader(privateKey));
        var keyPair = pemReader.ReadObject() as AsymmetricCipherKeyPair;

        _publicCrt = keyPair?.Public ??
                     throw new ArgumentException("Cannot create public key crt parameters", nameof(privateKey));
        _privateCrt = keyPair?.Private ??
                      throw new ArgumentException("Cannot create private key crt parameters", nameof(privateKey));
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
        var forEncryption = action == Action.Encrypt;
        var crt = forEncryption ? _publicCrt : _privateCrt;

        var rsaEngine = new RsaEngine();
        rsaEngine.Init(forEncryption, crt);

        var encodedBytes = Encoding.UTF8.GetBytes(code);
        var decryptedBytes = rsaEngine.ProcessBlock(encodedBytes, 0, encodedBytes.Length) ??
                             throw new ArgumentException("Cannot encode security code with current private key",
                                 nameof(code));

        var decrypted = Encoding.UTF8.GetString(decryptedBytes);

        return await Task.FromResult(decrypted);
    }

    private enum Action
    {
        Decrypt,
        Encrypt
    }
}