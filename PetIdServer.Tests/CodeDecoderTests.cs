using PetIdServer.Application.Common.Services;
using PetIdServer.Tests.Extensions;

namespace PetIdServer.Tests;

public class CodeDecoderTests
{
    private const string PublicCodeExample1 = "123456789";

    private readonly ICodeDecoder _codeDecoder =
        TestDependencies.GetRequiredService<ICodeDecoder>() ??
        throw new NullReferenceException(nameof(_codeDecoder));

    [Test]
    public async Task EncodingAndDecodingEndsWithInitialValueTest()
    {
        var encoded = await _codeDecoder.EncodePublicCode(PublicCodeExample1);
        var decoded = await _codeDecoder.GetPublicCodeOriginal(encoded);

        await Assert.That(decoded).IsEqualTo(PublicCodeExample1);
    }
}
