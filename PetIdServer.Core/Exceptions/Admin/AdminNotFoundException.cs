namespace PetIdServer.Core.Exceptions.Admin;

public class AdminNotFoundException : CoreException
{
    public override string Code { get; protected set; } = CoreExceptionCode.AdminNotFound;
    public override CoreExceptionKind? Kind => CoreExceptionKind.EntityNotFound;
    
    public AdminNotFoundException(string message = "Admin not found") : base(message) { }
    public AdminNotFoundException(object metadata) : base(metadata) { }
    public AdminNotFoundException(string message, object metadata): base(message, metadata) { }
}