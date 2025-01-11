using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Users.Exceptions;

public class UserAlreadyRegisteredException : CoreException
{
    public UserAlreadyRegisteredException(string message = "User already registered") :
        base(message)
    {
    }

    public UserAlreadyRegisteredException(object metadata) : base(metadata) { }

    public UserAlreadyRegisteredException(string message, object metadata) : base(message,
                                                                                  metadata)
    {
    }

    public override string Code { get; protected set; } = CoreExceptionCode.UserAlreadyRegistered;
    public override CoreExceptionKind? Kind => CoreExceptionKind.EntitiesConflicting;
}
