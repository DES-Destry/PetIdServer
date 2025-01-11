namespace PetIdServer.Core.Common;

public abstract record DomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public string EventName => GetType().Name;
    public DateTime OccurredOnItc { get; } = DateTime.UtcNow;
}
