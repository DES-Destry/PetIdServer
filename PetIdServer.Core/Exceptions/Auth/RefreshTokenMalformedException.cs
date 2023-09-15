namespace PetIdServer.Core.Exceptions.Auth;

public class RefreshTokenMalformedException : CoreException
{
    public override string Code { get; protected set; } = CoreExceptionCode.RefreshTokenMalformed;
    public override CoreExceptionKind? Kind => CoreExceptionKind.UserAuthenticationRequired;
    
    public RefreshTokenMalformedException(string message = "Refresh token is not valid!") : base(message) { }
    public RefreshTokenMalformedException(object metadata) : base(metadata) { }
    public RefreshTokenMalformedException(string message, object metadata): base(message, metadata) { }
}