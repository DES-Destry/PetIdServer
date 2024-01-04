namespace PetIdServer.Core.Common.Exceptions.Common;

public class ValidationException : CoreException
{
    public ValidationException(string message = "Validation exception") : base(message)
    {
    }

    public ValidationException(object metadata) : base(metadata)
    {
    }

    public ValidationException(string message, object metadata) : base(message, metadata)
    {
    }

    public override string Code { get; protected set; } = CoreExceptionCode.ValidationException;
    public override CoreExceptionKind? Kind => CoreExceptionKind.UserInputIsNotValid;
}