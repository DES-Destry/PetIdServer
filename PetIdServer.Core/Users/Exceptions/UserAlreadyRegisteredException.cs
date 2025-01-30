using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Users.Exceptions;

public sealed class UserAlreadyRegisteredException : CoreException
{
    private const string DefaultMessage = "User already registered";

    public UserAlreadyRegisteredException(string message = DefaultMessage) : base(message) { }
    public UserAlreadyRegisteredException(object metadata) : base(metadata) { }
    public UserAlreadyRegisteredException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = CoreExceptionCode.UserAlreadyRegistered;
    public override ExceptionKind? Kind => ExceptionKind.EntitiesConflicting;
}
