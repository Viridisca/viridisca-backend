using Viridisca.Modules.Grading.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Viridisca.Modules.Grading.Infrastructure.EF
{
    internal sealed class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.ToTable("grades");

            builder.HasKey(x => x.Uid);
            
            builder.Property(x => x.Uid)
                .HasColumnName("uid")
                .ValueGeneratedNever();
                
            builder.Property(x => x.StudentUid)
                .HasColumnName("student_uid")
                .IsRequired();
                
            builder.Property(x => x.SubjectUid)
                .HasColumnName("subject_uid")
                .IsRequired();
                
            builder.Property(x => x.TeacherUid)
                .HasColumnName("teacher_uid")
                .IsRequired();
                
            builder.Property(x => x.LessonUid)
                .HasColumnName("lesson_uid");
                
            builder.Property(x => x.Value)
                .HasColumnName("value")
                .HasColumnType("decimal(5,2)")
                .IsRequired();
                
            builder.Property(x => x.Description)
                .HasColumnName("description");
                
            builder.Property(x => x.Type)
                .HasColumnName("type")
                .HasConversion<string>()
                .IsRequired();
                
            builder.Property(x => x.IssuedAtUtc)
                .HasColumnName("issued_at_utc")
                .IsRequired();
                
            builder.Property(x => x.IsPublished)
                .HasColumnName("is_published")
                .IsRequired();
                
            builder.Property(x => x.PublishedAtUtc)
                .HasColumnName("published_at_utc");
                
            builder.Property(x => x.CreatedAtUtc)
                .HasColumnName("created_at_utc")
                .IsRequired();
                
            builder.Property(x => x.LastModifiedAtUtc)
                .HasColumnName("last_modified_at_utc");
                
            // Конфигурация связей
            builder.HasMany(x => x.Comments)
                .WithOne()
                .HasForeignKey(c => c.GradeUid)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasMany(x => x.Revisions)
                .WithOne()
                .HasForeignKey(r => r.GradeUid)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 