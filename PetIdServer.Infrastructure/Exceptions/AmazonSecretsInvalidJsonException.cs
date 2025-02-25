using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Infrastructure.Exceptions;

public class AmazonSecretsInvalidJsonException : InfrastructureException
{
    private const string DefaultMessage =
        "Amazon Secrets provided some data, but it cannot be deserialized as a JSON!";

    public AmazonSecretsInvalidJsonException(string message = DefaultMessage) : base(message) { }
    public AmazonSecretsInvalidJsonException(object metadata) : base(metadata) { }
    public AmazonSecretsInvalidJsonException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = InfrastructureExceptionCode.AmazonSecretsInvalidJson;
    public override ExceptionKind? Kind => ExceptionKind.Default;
}
