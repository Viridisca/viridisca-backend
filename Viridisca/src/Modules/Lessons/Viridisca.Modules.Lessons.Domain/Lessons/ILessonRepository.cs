namespace Viridisca.Modules.Lessons.Domain.Lessons;

public interface ILessonRepository
{
    Task<Lesson?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Lesson lesson);
}
