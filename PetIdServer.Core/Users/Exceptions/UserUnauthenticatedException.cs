using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Users.Exceptions;

public sealed class UserUnauthenticatedException : CoreException
{
    private const string DefaultMessage = "Unauthenticated";

    public UserUnauthenticatedException(string message = DefaultMessage) : base(message) { }
    public UserUnauthenticatedException(object metadata) : base(metadata) { }
    public UserUnauthenticatedException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = CoreExceptionCode.Unauthenticated;
    public override ExceptionKind? Kind => ExceptionKind.UserAuthenticationRequired;
}
