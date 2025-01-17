namespace PetIdServer.Core.Common;

public abstract class AggregateRoot<T>(T id) : Entity<T>(id);
