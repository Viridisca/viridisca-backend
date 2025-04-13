using Viridisca.Common.Domain;

namespace Viridisca.Modules.Lessons.Domain.Lessons;

public sealed class LessonRescheduledDomainEvent(Guid lessonId, DateTime startsAtUtc, DateTime? endsAtUtc)
    : DomainEvent
{
    public Guid LessonId { get; } = lessonId;

    public DateTime StartsAtUtc { get; } = startsAtUtc;

    public DateTime? EndsAtUtc { get; } = endsAtUtc;
}
