using Viridisca.Modules.Grading.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Viridisca.Modules.Grading.Infrastructure.EF
{
    internal sealed class GradeCommentConfiguration : IEntityTypeConfiguration<GradeComment>
    {
        public void Configure(EntityTypeBuilder<GradeComment> builder)
        {
            builder.ToTable("grade_comments");

            builder.HasKey(x => x.Uid);
            
            builder.Property(x => x.Uid)
                .HasColumnName("uid")
                .ValueGeneratedNever();
                
            builder.Property(x => x.GradeUid)
                .HasColumnName("grade_uid")
                .IsRequired();
                
            builder.Property(x => x.AuthorUid)
                .HasColumnName("author_uid")
                .IsRequired();
                
            builder.Property(x => x.Type)
                .HasColumnName("type")
                .HasConversion<string>()
                .IsRequired();
                
            builder.Property(x => x.Content)
                .HasColumnName("content")
                .IsRequired();
                
            builder.Property(x => x.CreatedAtUtc)
                .HasColumnName("created_at_utc")
                .IsRequired();
                
            builder.Property(x => x.LastModifiedAtUtc)
                .HasColumnName("last_modified_at_utc");
                
            builder.Property(x => x.IsDeleted)
                .HasColumnName("is_deleted")
                .IsRequired();
                
            builder.Property(x => x.DeletedAtUtc)
                .HasColumnName("deleted_at_utc");
                
            // Определяем отношение с Grade
            builder.HasOne<Grade>()
                .WithMany(g => g.Comments)
                .HasForeignKey(c => c.GradeUid)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 