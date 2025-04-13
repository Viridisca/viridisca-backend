using Viridisca.Common.Domain;

namespace Viridisca.Modules.Lessons.Domain.Lessons;

public sealed class LessonPublishedDomainEvent(Guid lessonId) : DomainEvent
{
    public Guid LessonId { get; init; } = lessonId;
}
