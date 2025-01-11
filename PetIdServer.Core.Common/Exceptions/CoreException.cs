namespace PetIdServer.Core.Common.Exceptions;

public abstract class CoreException : ScopedException
{
    protected CoreException() { }

    protected CoreException(string message) : base(message) { }

    protected CoreException(object metadata) { Metadata = metadata; }

    protected CoreException(string message, object metadata) : base(message)
    {
        Metadata = metadata;
    }

    protected override string DefaultScope => CoreExceptionCode.DefaultScope;

    public abstract CoreExceptionKind? Kind { get; }
    public object Metadata { get; set; }
}
