using Viridisca.Modules.Lessons.Domain.Lessons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Viridisca.Modules.Lessons.Infrastructure.Lessons;

internal sealed class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.HasKey(x => x.Uid);

        // builder.HasOne<Category>().WithMany();
    }
}
