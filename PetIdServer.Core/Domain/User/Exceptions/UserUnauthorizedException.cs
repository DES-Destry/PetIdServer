using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Domain.User.Exceptions;

public class UserUnauthorizedException : CoreException
{
    public UserUnauthorizedException(string message = "Unauthorized") : base(message) { }

    public UserUnauthorizedException(object metadata) : base(metadata) { }

    public UserUnauthorizedException(string message, object metadata) : base(message, metadata)
    {
    }

    public override string Code { get; protected set; } = CoreExceptionCode.Unauthorized;
    public override CoreExceptionKind? Kind => CoreExceptionKind.UserAuthorizationRequired;
}
