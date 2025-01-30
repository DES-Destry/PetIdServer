using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Users.Exceptions.Auth;

public sealed class AccessTokenMalformedException : CoreException
{
    private const string DefaultMessage = "Access token is not valid!";

    public AccessTokenMalformedException(string message = DefaultMessage) : base(message) { }
    public AccessTokenMalformedException(object metadata) : base(metadata) { }
    public AccessTokenMalformedException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = CoreExceptionCode.AccessTokenMalformed;
    public override ExceptionKind? Kind => ExceptionKind.UserAuthenticationRequired;
}
