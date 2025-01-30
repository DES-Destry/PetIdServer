using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Users.Exceptions;

public class UserUnauthenticatedException : CoreException
{
    public UserUnauthenticatedException(string message = "Unauthenticated") : base(message) { }

    public UserUnauthenticatedException(object metadata) : base(metadata) { }

    public UserUnauthenticatedException(string message, object metadata) : base(message, metadata)
    {
    }

    public override string Code { get; protected set; } = CoreExceptionCode.Unauthenticated;
    public override ExceptionKind? Kind => ExceptionKind.UserAuthenticationRequired;
}
