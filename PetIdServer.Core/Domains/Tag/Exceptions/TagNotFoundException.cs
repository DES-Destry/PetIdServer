using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Domains.Tag.Exceptions;

public class TagNotFoundException : CoreException
{
    public TagNotFoundException(string message = "Tag not found") : base(message) { }

    public TagNotFoundException(object metadata) : base(metadata) { }

    public TagNotFoundException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = CoreExceptionCode.TagNotFound;
    public override CoreExceptionKind? Kind => CoreExceptionKind.EntityNotFound;
}
