namespace PetIdServer.Core.Exceptions;

public enum CoreExceptionKind
{
    Default,
    UserInputIsNotValid,
    UserAuthenticationRequired,
    UserAuthorizationRequired,
    EntityNotFound,
    EntitiesConflicting,
    NotEnoughResources,
    
    MethodNotImplemented,
}