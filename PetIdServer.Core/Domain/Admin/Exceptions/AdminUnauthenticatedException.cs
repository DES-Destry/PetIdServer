using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Domain.Admin.Exceptions;

public class AdminUnauthenticatedException : CoreException
{
    public AdminUnauthenticatedException(string message = "Unauthenticated") : base(message) { }

    public AdminUnauthenticatedException(object metadata) : base(metadata) { }

    public AdminUnauthenticatedException(string message, object metadata) : base(message, metadata)
    {
    }

    public override string Code { get; protected set; } = CoreExceptionCode.Unauthenticated;
    public override CoreExceptionKind? Kind => CoreExceptionKind.UserAuthenticationRequired;
}
