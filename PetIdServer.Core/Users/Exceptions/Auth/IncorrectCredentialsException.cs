using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Users.Exceptions.Auth;

public sealed class IncorrectCredentialsException : CoreException
{
    private const string DefaultMessage = "Incorrect credentials provided!";

    public IncorrectCredentialsException(string message = DefaultMessage) : base(message) { }
    public IncorrectCredentialsException(object metadata) : base(metadata) { }
    public IncorrectCredentialsException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = CoreExceptionCode.IncorrectCredentials;
    public override ExceptionKind? Kind => ExceptionKind.UserAuthenticationRequired;
}
