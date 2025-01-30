using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Infrastructure.Exceptions;

/// <summary>
///     Class for secret key exceptions.
///     Make strange names to mislead clients, that these methods has security key requirement.
/// </summary>
public class YourMomIsBitchException : InfrastructureException
{
    private const string DefaultMessage = "Mom?";

    public YourMomIsBitchException(string message = DefaultMessage) : base(message) { }
    public YourMomIsBitchException(object metadata) : base(metadata) { }
    public YourMomIsBitchException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = InfrastructureExceptionCode.MomIsBitch;
    public override ExceptionKind? Kind => ExceptionKind.UserAuthorizationRequired;
}
