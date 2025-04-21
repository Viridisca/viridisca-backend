using Viridisca.Modules.Identity.Domain.Models;

namespace Viridisca.Modules.Identity.Application.Interfaces;

/// <summary>
/// Interface for JWT service
/// </summary>
public interface IJwtService
{
    /// <summary>
    /// Generates token for the user
    /// </summary>
    /// <param name="user">User to generate token for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Token response</returns>
    Task<TokenResponse> GenerateTokenAsync(User user, CancellationToken cancellationToken = default);
}

/// <summary>
/// Token response containing access and refresh tokens
/// </summary>
/// <param name="AccessToken">Access token</param>
/// <param name="RefreshToken">Refresh token</param>
/// <param name="ExpiresIn">Expiration time in seconds</param>
public record TokenResponse(string AccessToken, string RefreshToken, long ExpiresIn); 