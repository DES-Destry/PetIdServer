using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Pets.Exceptions;

public sealed class PetNotFoundException : CoreException
{
    private const string DefaultMessage = "Pet not found";

    public PetNotFoundException(string message = DefaultMessage) : base(message) { }
    public PetNotFoundException(object metadata) : base(metadata) { }
    public PetNotFoundException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = CoreExceptionCode.PetNotFound;
    public override ExceptionKind? Kind => ExceptionKind.EntityNotFound;
}
