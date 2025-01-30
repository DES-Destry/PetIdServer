using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Infrastructure.Exceptions;

public sealed class MisconfigurationException : InfrastructureException
{
    private const string DefaultMessage =
        "Misconfiguration occured. appsettings.json file doesn't filled completely!";

    public MisconfigurationException(string message = DefaultMessage) : base(message) { }
    public MisconfigurationException(object metadata) : base(metadata) { }
    public MisconfigurationException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = InfrastructureExceptionCode.Misconfiguration;
    public override ExceptionKind? Kind => ExceptionKind.Default;
}
