namespace PetIdServer.Core.Common.Exceptions;

public enum ExceptionKind
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
