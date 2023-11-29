namespace PetIdServer.Core.Exceptions.Tag;

public class TagAlreadyInUseException : CoreException
{
    public override string Code { get; protected set; } = CoreExceptionCode.TagAlreadyInUse;
    public override CoreExceptionKind? Kind => CoreExceptionKind.EntitiesConflicting;
    
    public TagAlreadyInUseException(string message = "Tag already in use, cannot perform virgin scan") : base(message) { }
    public TagAlreadyInUseException(object metadata) : base(metadata) { }
    public TagAlreadyInUseException(string message, object metadata): base(message, metadata) { }
}