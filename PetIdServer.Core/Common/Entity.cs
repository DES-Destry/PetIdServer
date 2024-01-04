namespace PetIdServer.Core.Common;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
{
    protected Entity(TId id)
    {
        ArgumentNullException.ThrowIfNull(id);

        if (Equals(id, default(TId)))
            throw new ArgumentException("The ID cannot be the default value.", nameof(id));

        Id = id;
    }

    public TId Id { get; protected init; }

    public bool Equals(Entity<TId>? other)
    {
        if (other == null || Id == null)
            return false;

        return Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is Entity<TId> entity) return Equals(entity);
        return false;
    }

    public override int GetHashCode() => Id?.GetHashCode() ?? 0;
}
