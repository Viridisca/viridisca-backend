using Viridisca.Modules.Lessons.Infrastructure.Database;
using Viridisca.Modules.Lessons.Domain.Lessons;
using Microsoft.EntityFrameworkCore; 

namespace Viridisca.Modules.Lessons.Infrastructure.Lessons;

internal sealed class LessonRepository(LessonsDbContext context) : ILessonRepository
{
    public Task<Lesson?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Lessons.FirstOrDefaultAsync(l => l.Uid == id, cancellationToken);
    }

    public void Insert(Lesson lesson)
    {
        context.Lessons.Add(lesson);
    }
}
