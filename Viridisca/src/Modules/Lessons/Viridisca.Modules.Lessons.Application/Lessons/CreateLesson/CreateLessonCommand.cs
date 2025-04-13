using MediatR;

namespace Viridisca.Modules.Lessons.Application.Lessons.CreateLesson;

public sealed record CreateLessonCommand(
    string Subject,
    string Description,
    string Classroom,
    DateTime StartsAtUtc,
    DateTime EndsAtUtc,
    Guid GroupUid,
    Guid TeacherUid) : IRequest<Guid>;
