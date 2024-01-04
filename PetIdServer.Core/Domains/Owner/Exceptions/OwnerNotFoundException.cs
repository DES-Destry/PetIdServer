using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Domains.Owner.Exceptions;

public class OwnerNotFoundException : CoreException
{
    public OwnerNotFoundException(string message = "Owner not found") : base(message) { }

    public OwnerNotFoundException(object metadata) : base(metadata) { }

    public OwnerNotFoundException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = CoreExceptionCode.OwnerNotFound;
    public override CoreExceptionKind? Kind => CoreExceptionKind.EntityNotFound;
}
