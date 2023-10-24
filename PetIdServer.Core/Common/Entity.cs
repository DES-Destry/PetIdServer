namespace PetIdServer.Core.Common;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
{
    private readonly TId _id;

    protected Entity(TId id)
    {
        ArgumentNullException.ThrowIfNull(id);
        
        if (Equals(id, default(TId)))
            throw new ArgumentException("The ID cannot be the default value.", nameof(id));

        _id = id;
    }

    public TId Id => _id;

    public bool Equals(Entity<TId>? other)
    {
        if (other == null || Id == null)
            return false;

        return Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is Entity<TId> entity)
        {
            return Equals(entity);
        }
        
        return base.Equals(obj);
    }

    public override int GetHashCode() => Id?.GetHashCode() ?? 0;
}