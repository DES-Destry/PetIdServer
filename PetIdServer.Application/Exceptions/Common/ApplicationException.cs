using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Application.Exceptions.Common;

public abstract class ApplicationException<TConcreteExceptionType> : ScopedException
{
    protected ApplicationException() { }

    protected ApplicationException(string message) : base(message) { }

    protected ApplicationException(object metadata) { Metadata = metadata; }

    protected ApplicationException(string message, object metadata) : base(message)
    {
        Metadata = metadata;
    }

    private string ApplicationMessage { get; set; }

    protected override string DefaultScope => ApplicationExceptionCode.Scope;

    public abstract CoreExceptionKind? Kind { get; }

    public override string Message => ApplicationMessage;
    public object Metadata { get; set; }

    protected ApplicationException<TConcreteExceptionType> WithMessageBase(string message)
    {
        ApplicationMessage = message;
        return this;
    }

    protected ApplicationException<TConcreteExceptionType> WithMetaBase(object metadata)
    {
        Metadata = metadata;
        return this;
    }

    public abstract TConcreteExceptionType WithMessage(string message);

    public abstract TConcreteExceptionType WithMeta(object metadata);
}
