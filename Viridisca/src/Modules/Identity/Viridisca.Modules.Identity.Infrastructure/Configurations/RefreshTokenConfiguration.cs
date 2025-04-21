using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Viridisca.Modules.Identity.Domain.Models;

namespace Viridisca.Modules.Identity.Infrastructure.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("refresh_tokens");

        builder.HasKey(rt => rt.Uid);

        builder.Property(rt => rt.Token)
            .HasMaxLength(128)
            .IsRequired();

        builder.HasIndex(rt => rt.Token)
            .IsUnique();

        builder.Property(rt => rt.ExpiryDate)
            .IsRequired();

        builder.HasOne<User>()
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UserUid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
} 