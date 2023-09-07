using PetIdServer.Core.Exceptions;

namespace PetIdServer.Application.Exceptions;

public abstract class ApplicationException : ScopedException
{
    protected override string DefaultScope => ApplicationExceptionCode.DefaultScope;
    
    public abstract CoreExceptionKind? Kind { get; }
    public object Metadata { get; set; }

    protected ApplicationException() { }
    protected ApplicationException(string message) : base(message) { }
    protected ApplicationException(object metadata)
    {
        Metadata = metadata;
    }
    protected ApplicationException(string message, object metadata) : base(message)
    {
        Metadata = metadata;
    }
}