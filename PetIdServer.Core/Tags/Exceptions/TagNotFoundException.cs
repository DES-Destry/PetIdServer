using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Tags.Exceptions;

public sealed class TagNotFoundException : CoreException
{
    private const string DefaultMessage = "Tag not found";

    public TagNotFoundException(string message = DefaultMessage) : base(message) { }
    public TagNotFoundException(object metadata) : base(metadata) { }
    public TagNotFoundException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = CoreExceptionCode.TagNotFound;
    public override ExceptionKind? Kind => ExceptionKind.EntityNotFound;
}
