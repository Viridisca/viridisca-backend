using Viridisca.Modules.Identity.Domain.Models;

namespace Viridisca.Modules.Identity.Domain.Repositories;

public interface IUserRepository
{
    /// <summary>
    /// Get user by ID
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>User or null if not found</returns>
    Task<User> GetByUidAsync(Guid uid, CancellationToken cancellationToken = default);
    /// <summary>
    /// Get user by email
    /// </summary>
    /// <param name="email">User email</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>User or null if not found</returns>
    Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all users
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of users</returns>
    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Check if user exists by email
    /// </summary>
    /// <param name="email">User email</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if exists, false otherwise</returns>
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetUsersByRoleAsync(RoleType role, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetUsersByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
   
    /// <summary>
    /// Add new user
    /// </summary>
    /// <param name="user">User to add</param>
    void Insert(User user);

    /// <summary>
    /// Update user
    /// </summary>
    /// <param name="user">User to update</param>
    void Update(User user);

    /// <summary>
    /// Remove user
    /// </summary>
    /// <param name="user">User to remove</param>
    void Delete(User user);  

    /// <summary>
    /// Get refresh token by token string
    /// </summary>
    /// <param name="token">Token string</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Refresh token or null if not found</returns>
    Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add new refresh token
    /// </summary>
    /// <param name="refreshToken">Refresh token to add</param>
    void AddRefreshToken(RefreshToken refreshToken);

    /// <summary>
    /// Remove refresh token
    /// </summary>
    /// <param name="refreshToken">Refresh token to remove</param>
    void RemoveRefreshToken(RefreshToken refreshToken);
}
