using MediatR;

namespace Viridisca.Modules.Lessons.Application.Lessons.GetLesson;

public sealed record GetLessonQuery(Guid LessonId) : IRequest<LessonResponse?>;
