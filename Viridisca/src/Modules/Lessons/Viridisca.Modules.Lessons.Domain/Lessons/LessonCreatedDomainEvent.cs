using Viridisca.Common.Domain;

namespace Viridisca.Modules.Lessons.Domain.Lessons;

public sealed class LessonCreatedDomainEvent(Guid lessonId) : DomainEvent
{
    public Guid LessonId { get; init; } = lessonId;
}
