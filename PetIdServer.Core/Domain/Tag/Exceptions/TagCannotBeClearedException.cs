using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Domain.Tag.Exceptions;

public class TagCannotBeClearedException : CoreException
{
    public TagCannotBeClearedException(string message = "Tag cannot be cleared") : base(message) { }

    public TagCannotBeClearedException(object metadata) : base(metadata) { }

    public TagCannotBeClearedException(string message, object metadata) : base(message, metadata)
    {
    }

    public override string Code { get; protected set; } = CoreExceptionCode.TagCannotBeCleared;
    public override CoreExceptionKind? Kind => CoreExceptionKind.UserAuthorizationRequired;
}
