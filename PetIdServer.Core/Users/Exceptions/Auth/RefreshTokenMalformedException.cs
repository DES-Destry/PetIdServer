using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Users.Exceptions.Auth;

public sealed class RefreshTokenMalformedException : CoreException
{
    private const string DefaultMessage = "Refresh token is not valid!";

    public RefreshTokenMalformedException(string message = DefaultMessage) : base(message) { }
    public RefreshTokenMalformedException(object metadata) : base(metadata) { }
    public RefreshTokenMalformedException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = CoreExceptionCode.RefreshTokenMalformed;
    public override ExceptionKind? Kind => ExceptionKind.UserAuthenticationRequired;
}
