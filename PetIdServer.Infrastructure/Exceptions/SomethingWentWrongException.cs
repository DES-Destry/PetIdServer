using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Infrastructure.Exceptions;

public sealed class SomethingWentWrongException : InfrastructureException
{
    private const string DefaultMessage = "Something went wrong";

    public SomethingWentWrongException(string message = DefaultMessage) : base(message) { }
    public SomethingWentWrongException(object metadata) : base(metadata) { }
    public SomethingWentWrongException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = InfrastructureExceptionCode.SomethingWentWrong;
    public override ExceptionKind? Kind => ExceptionKind.Default;
}
