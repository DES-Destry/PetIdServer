namespace PetIdServer.Core.Exceptions.Auth;

public class AccessTokenMalformedException : CoreException
{
    public override string Code { get; protected set; } = CoreExceptionCode.AccessTokenMalformed;
    public override CoreExceptionKind? Kind => CoreExceptionKind.UserAuthenticationRequired;
    
    public AccessTokenMalformedException(string message = "Access token is not valid!") : base(message) { }
    public AccessTokenMalformedException(object metadata) : base(metadata) { }
    public AccessTokenMalformedException(string message, object metadata): base(message, metadata) { }
}