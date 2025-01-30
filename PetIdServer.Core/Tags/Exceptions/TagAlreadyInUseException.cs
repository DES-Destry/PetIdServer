using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Tags.Exceptions;

public sealed class TagAlreadyInUseException : CoreException
{
    private const string DefaultMessage = "Tag already in use, cannot perform virgin scan";

    public TagAlreadyInUseException(string message = DefaultMessage) : base(message) { }
    public TagAlreadyInUseException(object metadata) : base(metadata) { }
    public TagAlreadyInUseException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = CoreExceptionCode.TagAlreadyInUse;
    public override ExceptionKind? Kind => ExceptionKind.EntitiesConflicting;
}
