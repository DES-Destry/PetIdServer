namespace PetIdServer.Core.Exceptions.Admin;

public class AdminAlreadyCreatedException : CoreException
{
    public override string Code { get; protected set; } = CoreExceptionCode.AdminAlreadyCreated;
    public override CoreExceptionKind? Kind => CoreExceptionKind.EntitiesConflicting;
    
    public AdminAlreadyCreatedException(string message = "Admin already created") : base(message) { }
    public AdminAlreadyCreatedException(object metadata) : base(metadata) { }
    public AdminAlreadyCreatedException(string message, object metadata): base(message, metadata) { }
}