using PetIdServer.Application.Common.Services;
using PetIdServer.Tests.Extensions;

namespace PetIdServer.Tests;

public class HashTests
{
    private readonly IHashService _hashService = TestDependencies.GetRequiredService<IHashService>();

    [Test]
    public async Task HashesShouldValidate()
    {
        const string example = "1234567890";

        string hash = await _hashService.Hash(example);
        bool isValidated = await _hashService.Validate(example, hash);

        await Assert.That(isValidated).IsTrue();
    }

    [Test]
    public async Task HashesShouldFilterWrongPass()
    {
        const string example = "1234567890";
        const string wrongExample = "12345678901234567890";

        string hash = await _hashService.Hash(example);
        bool isValidated = await _hashService.Validate(wrongExample, hash);

        await Assert.That(isValidated).IsFalse();
    }

    [Test]
    public async Task HashesShouldNotBeEqual()
    {
        const string example = "1234567890";

        string hash1 = await _hashService.Hash(example);
        string hash2 = await _hashService.Hash(example);

        await Assert.That(hash1).IsNotEqualTo(hash2);
    }
}
