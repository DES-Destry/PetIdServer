using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Domain.Pet.Exceptions;

public class PetNotFoundException : CoreException
{
    public PetNotFoundException(string message = "Pet not found") : base(message) { }

    public PetNotFoundException(object metadata) : base(metadata) { }

    public PetNotFoundException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = CoreExceptionCode.PetNotFound;
    public override CoreExceptionKind? Kind => CoreExceptionKind.EntityNotFound;
}
