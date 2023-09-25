namespace PetIdServer.Core.Exceptions.Auth;

public class IncorrectCredentialsException : CoreException
{
    public override string Code { get; protected set; } = CoreExceptionCode.IncorrectCredentials;
    public override CoreExceptionKind? Kind => CoreExceptionKind.UserAuthenticationRequired;


    public IncorrectCredentialsException(string message = "Incorrect credentials provided!") : base(message) { }
    public IncorrectCredentialsException(object metadata) : base(metadata) { }
    public IncorrectCredentialsException(string message, object metadata): base(message, metadata) { }
}