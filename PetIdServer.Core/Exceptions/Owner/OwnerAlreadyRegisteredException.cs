namespace PetIdServer.Core.Exceptions.Owner;

public class OwnerAlreadyRegisteredException : CoreException
{
    public override string Code { get; protected set; } = CoreExceptionCode.OwnerAlreadyRegistered;
    public override CoreExceptionKind? Kind => CoreExceptionKind.EntitiesConflicting;
    
    public OwnerAlreadyRegisteredException(string message = "Owner already registered") : base(message) { }
    public OwnerAlreadyRegisteredException(object metadata) : base(metadata) { }
    public OwnerAlreadyRegisteredException(string message, object metadata): base(message, metadata) { }
}