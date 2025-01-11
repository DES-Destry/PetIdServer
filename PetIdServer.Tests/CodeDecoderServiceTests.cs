using PetIdServer.Application.Tag.Services;
using PetIdServer.Tests.Extensions;

namespace PetIdServer.Tests;

public class CodeDecoderServiceTests
{
    private readonly ICodeDecoder _codeDecoderService = TestDependencies.GetRequiredService<ICodeDecoder>();

    [Test]
    public async Task ShouldEncodeAndDecode()
    {
        const string example = "1234567890";

        string privateCode = await _codeDecoderService.EncodePublicCode(example);
        string publicCode = await _codeDecoderService.GetPublicCodeOriginal(privateCode);

        await Assert.That(publicCode).IsEqualTo(example);
    }

    [Test]
    public async Task ShouldNotDecodeAndEncode()
    {
        const string example =
            "kFkqWpYvmOXwnhXz/jNO+o6qFvCn0HeX1umpErdu0HETxJ8SHHw7zN6JIoPJN5LC7XfsVR/pGwK8E2xF/qOUI5cbCKXEXdakjmZk4SsIwB9XvVQfRLggCmf7XmyZsORr6EV4wRMxkwy885fWxAef6gwQYdqjST4wfQaIeNhFDwjAqHJvEqfNhEd2dLefrt6ZPoR739NlM+ZDIK3mtoM3R3eiKPRFycMWETJZ6G3PWNxTqKhX+0b4pBGIhcDkBrf1X7xuTTySgPk/1V84X62v9k4ldMFNSEL2pIfIb5vLP71spdinrH/psdyPJzkVVsMFgk3qCIbnzxWtpQlbZCRfSQ==";

        string publicCode = await _codeDecoderService.GetPublicCodeOriginal(example);
        string privateCode = await _codeDecoderService.EncodePublicCode(publicCode);

        await Assert.That(privateCode).IsNotEqualTo(example);
    }
}
