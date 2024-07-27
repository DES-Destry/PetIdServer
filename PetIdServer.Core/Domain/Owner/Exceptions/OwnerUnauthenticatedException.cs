using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Domain.Owner.Exceptions;

public class OwnerUnauthenticatedException : CoreException
{
    public OwnerUnauthenticatedException(string message = "Unauthenticated") : base(message) { }

    public OwnerUnauthenticatedException(object metadata) : base(metadata) { }

    public OwnerUnauthenticatedException(string message, object metadata) : base(message, metadata)
    {
    }

    public override string Code { get; protected set; } = CoreExceptionCode.Unauthenticated;
    public override CoreExceptionKind? Kind => CoreExceptionKind.UserAuthenticationRequired;
}
