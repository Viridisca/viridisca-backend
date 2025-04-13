using Viridisca.Modules.Grading.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Viridisca.Modules.Grading.Infrastructure.EF
{
    internal sealed class GradeRevisionConfiguration : IEntityTypeConfiguration<GradeRevision>
    {
        public void Configure(EntityTypeBuilder<GradeRevision> builder)
        {
            builder.ToTable("grade_revisions");

            builder.HasKey(x => x.Uid);
            
            builder.Property(x => x.Uid)
                .HasColumnName("uid")
                .ValueGeneratedNever();
                
            builder.Property(x => x.GradeUid)
                .HasColumnName("grade_uid")
                .IsRequired();
                
            builder.Property(x => x.TeacherUid)
                .HasColumnName("teacher_uid")
                .IsRequired();
                
            builder.Property(x => x.PreviousValue)
                .HasColumnName("previous_value")
                .IsRequired();
                
            builder.Property(x => x.NewValue)
                .HasColumnName("new_value")
                .IsRequired();
                
            builder.Property(x => x.PreviousDescription)
                .HasColumnName("previous_description");
                
            builder.Property(x => x.NewDescription)
                .HasColumnName("new_description");
                
            builder.Property(x => x.RevisionReason)
                .HasColumnName("revision_reason");
                
            builder.Property(x => x.CreatedAtUtc)
                .HasColumnName("created_at_utc")
                .IsRequired();
                
            // Определяем отношение с Grade
            builder.HasOne<Grade>()
                .WithMany(g => g.Revisions)
                .HasForeignKey(r => r.GradeUid)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 