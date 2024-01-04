using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Domain.Admin.Exceptions;

public class AdminNotFoundException : CoreException
{
    public AdminNotFoundException(string message = "Admin not found") : base(message) { }

    public AdminNotFoundException(object metadata) : base(metadata) { }

    public AdminNotFoundException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = CoreExceptionCode.AdminNotFound;
    public override CoreExceptionKind? Kind => CoreExceptionKind.EntityNotFound;
}
