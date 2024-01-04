namespace PetIdServer.Core.Common.Exceptions;

public enum CoreExceptionKind
{
    Default,
    UserInputIsNotValid,
    UserAuthenticationRequired,
    UserAuthorizationRequired,
    EntityNotFound,
    EntitiesConflicting,
    NotEnoughResources,

    MethodNotImplemented
}