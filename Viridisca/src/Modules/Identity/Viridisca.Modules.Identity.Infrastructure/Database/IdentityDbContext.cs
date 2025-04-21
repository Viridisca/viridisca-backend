using Microsoft.EntityFrameworkCore;
using Viridisca.Common.Application.Data;
using Viridisca.Common.Infrastructure.EF;
using Viridisca.Modules.Identity.Domain.Models;

namespace Viridisca.Modules.Identity.Infrastructure.Database;

/// <summary>
/// Database context for the Identity module
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="IdentityDbContext"/> class.
/// </remarks>
/// <param name="options">The options to be used by the context.</param>
public sealed class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : DbContext(options), IUnitOfWork
{
    /// <summary>
    /// Gets or sets the users.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Gets or sets the roles.
    /// </summary>
    public DbSet<Role> Roles { get; set; }

    /// <summary>
    /// Gets or sets the user roles.
    /// </summary>
    public DbSet<UserRole> UserRoles { get; set; }

    /// <summary>
    /// Gets or sets the refresh tokens.
    /// </summary>
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    /// <summary>
    /// Gets or sets the outbox messages.
    /// </summary>
    // public DbSet<OutboxMessage> OutboxMessages { get; set; }

    /// <summary>
    /// Configures the model for the context.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Identity); 
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
    }  
}