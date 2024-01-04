using PetIdServer.Core.Common.Exceptions;

namespace PetIdServer.Core.Domains.Tag.Exceptions;

public class TagAlreadyCreatedException : CoreException
{
    public TagAlreadyCreatedException(string message = "Tag already exists") : base(message) { }

    public TagAlreadyCreatedException(object metadata) : base(metadata) { }

    public TagAlreadyCreatedException(string message, object metadata) : base(message, metadata) { }

    public override string Code { get; protected set; } = CoreExceptionCode.TagAlreadyExists;
    public override CoreExceptionKind? Kind => CoreExceptionKind.EntitiesConflicting;
}
