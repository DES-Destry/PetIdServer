using PetIdServer.Core.Exceptions;

namespace PetIdServer.Application.Exceptions;

public class SomethingWentWrongException : ApplicationException
{
    public override string Code { get; protected set; } = ApplicationExceptionCode.SomethingWentWrong;
    public override CoreExceptionKind? Kind => CoreExceptionKind.Default;
    
    public SomethingWentWrongException(string message = "Something went wrong") : base(message) { }
    public SomethingWentWrongException(object metadata) : base(metadata) { }
    public SomethingWentWrongException(string message, object metadata): base(message, metadata) { }
}