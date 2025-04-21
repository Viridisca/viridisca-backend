using Microsoft.EntityFrameworkCore;
using Viridisca.Modules.Identity.Domain.Models;
using Viridisca.Modules.Identity.Domain.Repositories;
using Viridisca.Modules.Identity.Infrastructure.Database;

namespace Viridisca.Modules.Identity.Infrastructure.Repositories;

/// <summary>
/// Repository for user entities
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="UserRepository"/> class
/// </remarks>
/// <param name="dbContext">The database context</param>
internal sealed class UserRepository(IdentityDbContext dbContext) : IUserRepository
{
    private readonly IdentityDbContext _dbContext = dbContext;

    /// <inheritdoc />
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Uid == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email == email, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public void Insert(User user)
    {
        _dbContext.Users.Add(user);
    }

    /// <inheritdoc />
    public void Update(User user)
    {
        _dbContext.Users.Update(user);
    }

    /// <inheritdoc />
    public void Remove(User user)
    {
        _dbContext.Users.Remove(user);
    }

    /// <inheritdoc />
    public async Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        return await _dbContext.RefreshTokens
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Token == token, cancellationToken);
    }

    /// <inheritdoc />
    public void AddRefreshToken(RefreshToken refreshToken)
    {
        _dbContext.RefreshTokens.Add(refreshToken);
    }

    /// <inheritdoc />
    public void RemoveRefreshToken(RefreshToken refreshToken)
    {
        _dbContext.RefreshTokens.Remove(refreshToken);
    }

    public Task<User> GetByUidAsync(Guid uid, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetUsersByRoleAsync(RoleType role, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetUsersByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Delete(User user)
    {
        throw new NotImplementedException();
    }
} 