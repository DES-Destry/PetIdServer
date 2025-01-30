using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Users.Exceptions;

public sealed class UserUnauthorizedException : CoreException
{
    private const string DefaultMessage = "Unauthorized";

    public UserUnauthorizedException(string message = DefaultMessage) : base(message) { }
    public UserUnauthorizedException(object metadata) : base(metadata) { }
    public UserUnauthorizedException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = CoreExceptionCode.Unauthorized;
    public override ExceptionKind? Kind => ExceptionKind.UserAuthorizationRequired;
}
