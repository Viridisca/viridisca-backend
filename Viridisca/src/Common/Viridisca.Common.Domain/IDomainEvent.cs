namespace Viridisca.Common.Domain;

public interface IDomainEvent
{
    Guid Id { get; }
    DateTime OccurredOnUtc { get; }
    // string EventType { get; }
}
