using Viridisca.Common.Application.Data;
using Viridisca.Modules.Lessons.Domain.Lessons;
using Viridisca.Modules.Lessons.Infrastructure.Lessons;
using Microsoft.EntityFrameworkCore;

namespace Viridisca.Modules.Lessons.Infrastructure.Database;

public sealed class LessonsDbContext(DbContextOptions<LessonsDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<Lesson> Lessons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Lessons);

        modelBuilder.ApplyConfiguration(new LessonConfiguration());
    }
}

