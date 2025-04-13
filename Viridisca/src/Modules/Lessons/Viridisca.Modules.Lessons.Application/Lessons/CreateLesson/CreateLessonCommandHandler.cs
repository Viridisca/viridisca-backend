using Viridisca.Common.Application.Data;
using Viridisca.Modules.Lessons.Domain.Lessons;
using MediatR;

namespace Viridisca.Modules.Lessons.Application.Lessons.CreateLesson;

internal sealed class CreateLessonCommandHandler(ILessonRepository lessonRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateLessonCommand, Guid>
{
    public async Task<Guid> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = Lesson.Create(
            request.Subject,
            request.Description,
            request.Classroom,
            request.StartsAtUtc,
            request.EndsAtUtc,
            request.GroupUid,
            request.TeacherUid);

        lessonRepository.Insert(lesson.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return lesson.Value.Uid;
    }

}
