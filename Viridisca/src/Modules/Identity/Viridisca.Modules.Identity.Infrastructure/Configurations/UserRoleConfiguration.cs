using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Viridisca.Modules.Identity.Domain.Models;

namespace Viridisca.Modules.Identity.Infrastructure.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("user_roles");

        builder.HasKey(ur => ur.Uid);

        builder.Property(ur => ur.Role.RoleType)
            .IsRequired()
            .HasConversion<int>();

        builder.HasOne<User>()
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserUid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
} 