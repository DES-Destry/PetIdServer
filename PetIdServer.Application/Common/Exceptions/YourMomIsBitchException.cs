using PetIdServer.Application.Common.Exceptions.Common;
using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Application.Common.Exceptions;

/// <summary>
///     Class for secret key exceptions.
///     Make strange names to mislead clients, that these methods has security key requirement.
/// </summary>
public class YourMomIsBitchException : ApplicationException<YourMomIsBitchException>
{
    public const string DefaultMessage = "Mom?";

    public YourMomIsBitchException(string message = DefaultMessage) : base(message) { }

    public YourMomIsBitchException(object metadata) : base(metadata) { }

    public YourMomIsBitchException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = ApplicationExceptionCode.MomIsBitch;
    public override CoreExceptionKind? Kind => CoreExceptionKind.UserAuthorizationRequired;

    public override YourMomIsBitchException WithMessage(string message)
    {
        WithMessageBase(message);
        return this;
    }

    public override YourMomIsBitchException WithMeta(object metadata)
    {
        WithMetaBase(metadata);
        return this;
    }
}
