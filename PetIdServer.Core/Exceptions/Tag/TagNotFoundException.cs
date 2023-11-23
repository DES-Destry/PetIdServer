namespace PetIdServer.Core.Exceptions.Tag;

public class TagNotFoundException : CoreException
{
    public override string Code { get; protected set; } = CoreExceptionCode.TagAlreadyInUse;
    public override CoreExceptionKind? Kind => CoreExceptionKind.EntityNotFound;
    
    public TagNotFoundException(string message = "Tag not found") : base(message) { }
    public TagNotFoundException(object metadata) : base(metadata) { }
    public TagNotFoundException(string message, object metadata): base(message, metadata) { }
}