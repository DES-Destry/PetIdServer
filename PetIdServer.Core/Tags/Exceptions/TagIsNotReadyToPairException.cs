using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Tags.Exceptions;

public sealed class TagIsNotReadyToPairException : CoreException
{
    private const string DefaultMessage = "Tag is not in status, that allows pairing";

    public TagIsNotReadyToPairException(string message = DefaultMessage) : base(message) { }
    public TagIsNotReadyToPairException(object metadata) : base(metadata) { }
    public TagIsNotReadyToPairException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = CoreExceptionCode.TagIsNotReadyToPair;
    public override ExceptionKind? Kind => ExceptionKind.EntitiesConflicting;
}
