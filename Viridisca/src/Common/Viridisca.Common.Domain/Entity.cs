namespace Viridisca.Common.Domain;

public abstract class Entity
{
    protected Entity() { }
     
    // Список доменных событий, связанных с сущностью
    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyCollection<IDomainEvent> DomainEvents => [.. _domainEvents];
     
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
     
    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
