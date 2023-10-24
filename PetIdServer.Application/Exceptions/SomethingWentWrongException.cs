using PetIdServer.Core.Exceptions;

namespace PetIdServer.Application.Exceptions;

public class SomethingWentWrongException : ApplicationException<SomethingWentWrongException>
{
    public const string DefaultErrorMessage = "Something went wrong";
    
    public override string Code { get; protected set; } = ApplicationExceptionCode.SomethingWentWrong;
    public override CoreExceptionKind? Kind => CoreExceptionKind.Default;

    public SomethingWentWrongException(string? message) : base(message ?? DefaultErrorMessage) { }
    public SomethingWentWrongException(object metadata) : base(metadata) { }
    public SomethingWentWrongException(string message, object metadata): base(message, metadata) { }
    
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