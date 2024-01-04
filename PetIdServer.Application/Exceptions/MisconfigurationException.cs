using PetIdServer.Application.Exceptions.Common;
using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Application.Exceptions;

public class MisconfigurationException : ApplicationException<MisconfigurationException>
{
    public const string DefaultMessage = "Misconfiguration occured. appsettings.json file doesn't filled completely!";

    public MisconfigurationException(string message = DefaultMessage) : base(message)
    {
    }

    public MisconfigurationException(object metadata) : base(metadata)
    {
    }

    public MisconfigurationException(string message, object metadata) : base(message, metadata)
    {
    }

    public override string Code { get; protected set; } = ApplicationExceptionCode.Misconfiguration;
    public override CoreExceptionKind? Kind => CoreExceptionKind.Default;

    public override MisconfigurationException WithMessage(string message)
    {
        WithMessageBase(message);
        return this;
    }

    public override MisconfigurationException WithMeta(object metadata)
    {
        WithMetaBase(metadata);
        return this;
    }
}