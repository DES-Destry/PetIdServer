using System.Reflection;
using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Application.Common.Exceptions;

public abstract class ApplicationException : ScopedException
{
    protected ApplicationException() { }

    protected ApplicationException(string message) : base(message) { }

    protected ApplicationException(object metadata) { Metadata = metadata; }

    protected ApplicationException(string message, object metadata) : base(message)
    {
        Metadata = metadata;
    }

    protected override string DefaultScope => ApplicationExceptionCode.Scope;

    public abstract ExceptionKind? Kind { get; }

    public object Metadata { get; set; } = new
    {
    };

    public ApplicationException WithMessage(string message)
    {
        ApplicationException exception = (ApplicationException)MemberwiseClone();
        Type badThingButNecessary = typeof(Exception);
        const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;

        FieldInfo? fieldInfo = badThingButNecessary.GetField("_message", flags);
        fieldInfo?.SetValue(exception, message);

        return exception;
    }

    public ApplicationException WithMeta(object metadata)
    {
        ApplicationException exception = (ApplicationException)MemberwiseClone();
        exception.Metadata = metadata;
        return exception;
    }
}
