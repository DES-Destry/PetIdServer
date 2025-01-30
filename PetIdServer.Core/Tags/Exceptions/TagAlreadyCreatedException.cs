using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Tags.Exceptions;

public sealed class TagAlreadyCreatedException : CoreException
{
    private const string DefaultMessage = "Tag already exists";

    public TagAlreadyCreatedException(string message = DefaultMessage) : base(message) { }
    public TagAlreadyCreatedException(object metadata) : base(metadata) { }
    public TagAlreadyCreatedException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = CoreExceptionCode.TagAlreadyExists;
    public override ExceptionKind? Kind => ExceptionKind.EntitiesConflicting;
}
