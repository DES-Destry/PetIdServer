using System.Reflection;
using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Infrastructure.Exceptions;

public abstract class InfrastructureException : ScopedException
{
    protected InfrastructureException() { }

    protected InfrastructureException(string message) : base(message) { }

    protected InfrastructureException(object metadata) { Metadata = metadata; }

    protected InfrastructureException(string message, object metadata) : base(message)
    {
        Metadata = metadata;
    }

    protected override string DefaultScope => InfrastructureExceptionCode.Scope;

    public abstract ExceptionKind? Kind { get; }

    public object Metadata { get; set; } = new
    {
    };

    public InfrastructureException WithMessage(string message)
    {
        InfrastructureException exception = (InfrastructureException)MemberwiseClone();
        Type badThingButNecessary = typeof(Exception);
        const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;

        FieldInfo? fieldInfo = badThingButNecessary.GetField("_message", flags);
        fieldInfo?.SetValue(exception, message);

        return exception;
    }

    public InfrastructureException WithMeta(object metadata)
    {
        InfrastructureException exception = (InfrastructureException)MemberwiseClone();
        exception.Metadata = metadata;
        return exception;
    }
}
