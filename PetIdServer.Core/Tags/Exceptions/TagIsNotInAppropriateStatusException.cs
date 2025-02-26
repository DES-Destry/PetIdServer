using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Tags.Exceptions;

public sealed class TagIsNotInAppropriateStatusException : CoreException
{
    private const string DefaultMessage = "Tag has such status, that does not allow this operation";

    public TagIsNotInAppropriateStatusException(string message = DefaultMessage) : base(message) { }
    public TagIsNotInAppropriateStatusException(object metadata) : base(metadata) { }
    public TagIsNotInAppropriateStatusException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = CoreExceptionCode.TagIsNotInAppropriateStatus;
    public override ExceptionKind? Kind => ExceptionKind.EntitiesConflicting;
}
