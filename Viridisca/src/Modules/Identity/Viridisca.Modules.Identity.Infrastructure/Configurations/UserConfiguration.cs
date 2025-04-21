using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Viridisca.Modules.Identity.Domain.Models;

namespace Viridisca.Modules.Identity.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Uid);
        
        builder.Property(u => u.Uid)
            .IsRequired();

        builder.HasIndex(u => u.Uid)
            .IsUnique();

        builder.Property(u => u.Email)
            .HasMaxLength(256)
            .IsRequired();

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.Username)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(u => u.Username)
            .IsUnique();

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.Property(u => u.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.MiddleName)
            .HasMaxLength(100);

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(u => u.ProfileImageUrl)
            .HasMaxLength(1000);

        builder.Property(u => u.SecurityStamp)
            .IsRequired();
    }
} 