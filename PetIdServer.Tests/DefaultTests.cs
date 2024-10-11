namespace PetIdServer.Tests;

public class DefaultTests
{
    // private const string AdminPanelGeneratedCode1 =
    //     "l2asizsVFfvHgEUhP9RAo/DpHT+uWHnAUC3r8Ewp8HR8q8LYKfBWt++KXZktLAlv0mpED+o02r+AlRHAV1FGOLrW7d6aBoxz3JxTOT+TxgrykgfC+RjpvdqyhnMJe53QJfNBj83LdgFeuR8fbSKosn5VcDYXYT9fULntHZskvngDNeJpHgIIFK0b89ikD7T/Zp/Brno4jM4vZKZBqgTWI3qcJCiSRkpC7maojH8MN2bvz7Vp3sWuOe9iHGtysN0rQAeTT1auKRPcH9Co3PXvFqUhTU5UuCJyQDeiSUxJ9WcEPsd6ta1zuvZXUz1l8HUoDpNH0T+gT6wJizm81wAuSg==";
    //
    // private const string AdminPanelGeneratedCode2 =
    //     "lWT1b1rMXsQXOAlvMsfCLysp2V1ySyMsk6obSI4D++rXTrj6P8mV8PX5ieBZeREe4Z5ouT0uHWGAZS3DJWqCFbXGhNEzkfXj3CTJeMjc6ZZCPA8I/rujptznZdFAzPm636tN3vit/RVpK0Cn63BU79G0tj7a3/y9Hn9PJibI0AHjfyCGlhUY6zlipYv/3CWc8ESAha2bMGtYyqFJzm+RlicUqvGR74dl/Zszp5bcV7991Ot62FR2onb29aFDGr3QHcMPIIi248LpUIovJsMNH0uE0v2HnEFL9QiAQ7CvbFqvjMBtc7736mAH4jCuvU7B6suoShvD+TNDWVpL9nqW8g==";

    [Test]
    public async Task SumTest()
    {
        var a = 2 + 2;
        await Assert.That(a).IsEqualTo(4);
    }
}
