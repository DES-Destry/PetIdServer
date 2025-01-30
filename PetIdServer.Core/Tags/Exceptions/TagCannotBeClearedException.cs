using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Tags.Exceptions;

public sealed class TagCannotBeClearedException : CoreException
{
    private const string DefaultMessage = "Tag cannot be cleared";

    public TagCannotBeClearedException(string message = DefaultMessage) : base(message) { }
    public TagCannotBeClearedException(object metadata) : base(metadata) { }
    public TagCannotBeClearedException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = CoreExceptionCode.TagCannotBeCleared;
    public override ExceptionKind? Kind => ExceptionKind.UserAuthorizationRequired;
}
