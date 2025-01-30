using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Users.Exceptions;

public sealed class UserNotFoundException : CoreException
{
    private const string DefaultMessage = "User not found";

    public UserNotFoundException(string message = DefaultMessage) : base(message) { }
    public UserNotFoundException(object metadata) : base(metadata) { }
    public UserNotFoundException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = CoreExceptionCode.UserNotFound;
    public override ExceptionKind? Kind => ExceptionKind.EntityNotFound;
}
