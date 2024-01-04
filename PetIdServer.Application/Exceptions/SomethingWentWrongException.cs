using PetIdServer.Application.Exceptions.Common;
using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Application.Exceptions;

public class SomethingWentWrongException : ApplicationException<SomethingWentWrongException>
{
    public const string DefaultMessage = "Something went wrong";

    public SomethingWentWrongException(string message = DefaultMessage) : base(message) { }

    public SomethingWentWrongException(object metadata) : base(metadata) { }

    public SomethingWentWrongException(string message, object metadata) : base(message, metadata)
    {
    }

    public override string Code { get; protected set; } =
        ApplicationExceptionCode.SomethingWentWrong;

    public override CoreExceptionKind? Kind => CoreExceptionKind.Default;

    public override SomethingWentWrongException WithMessage(string message)
    {
        WithMessageBase(message);
        return this;
    }

    public override SomethingWentWrongException WithMeta(object metadata)
    {
        WithMetaBase(metadata);
        return this;
    }
}
