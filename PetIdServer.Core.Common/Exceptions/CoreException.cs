using System.Reflection;

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

    public abstract ExceptionKind? Kind { get; }

    public object Metadata { get; set; } = new
    {
    };

    public CoreException WithMessage(string message)
    {
        CoreException exception = (CoreException)MemberwiseClone();
        Type badThingButNecessary = typeof(Exception);
        const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;

        FieldInfo? fieldInfo = badThingButNecessary.GetField("_message", flags);

        fieldInfo?.SetValue(exception, message);

        return exception;
    }

    public CoreException WithMeta(object metadata)
    {
        CoreException exception = (CoreException)MemberwiseClone();
        exception.Metadata = metadata;
        return exception;
    }
}
