using Microsoft.EntityFrameworkCore;
using Viridisca.Modules.Identity.Domain.Models;
using Viridisca.Modules.Identity.Domain.Repositories;
using Viridisca.Modules.Identity.Infrastructure.Database;

namespace Viridisca.Modules.Identity.Infrastructure.Users;

public class UserRepository : IUserRepository
{
    private readonly IdentityDbContext _dbContext;
    
    public UserRepository(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> GetByUidAsync(Guid uid, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Uid == uid, cancellationToken);
    }

    public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Email == email.ToLowerInvariant(), cancellationToken);
    }

    public async Task<User> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .AnyAsync(u => u.Email == email.ToLowerInvariant(), cancellationToken);
    }

    public async Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .AnyAsync(u => u.Username == username, cancellationToken);
    }

    public async Task<IEnumerable<User>> GetUsersByRoleAsync(RoleType roleType, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Where(u => u.UserRoles.Any(r => r.Role.RoleType == roleType))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<User>> GetUsersByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Include(u => u.RefreshTokens)
            .Where(u => u.RefreshTokens.Any(t => t.Token == refreshToken))
            .ToListAsync(cancellationToken);
    }

    public void Insert(User user)
    {
        _dbContext.Users.Add(user);
    } 

    public void Update(User user)
    {
        _dbContext.Users.Update(user);
    }

    public void Delete(User user)
    {
        _dbContext.Users.Remove(user);
    }
    
    public async Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        return await _dbContext.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken);
    }
    
    public void AddRefreshToken(RefreshToken refreshToken)
    {
        _dbContext.RefreshTokens.Add(refreshToken);
    }
    
    public void RemoveRefreshToken(RefreshToken refreshToken)
    {
        _dbContext.RefreshTokens.Remove(refreshToken);
    }
} 