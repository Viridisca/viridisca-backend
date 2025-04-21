using Viridisca.Common.Domain;
using Viridisca.Modules.Identity.Domain.Models;

namespace Viridisca.Modules.Identity.Domain.Services;

/// <summary>
/// Interface for JWT token provider
/// </summary>
public interface IJwtProvider
{
    /// <summary>
    /// Generates an access token for a user
    /// </summary>
    /// <param name="user">The user</param>
    /// <returns>The access token</returns>
    string GenerateAccessToken(User user);

    /// <summary>
    /// Generates a refresh token
    /// </summary>
    /// <returns>The refresh token</returns>
    string GenerateRefreshToken();
    
    /// <summary>
    /// Generates a token asynchronously
    /// </summary>
    /// <param name="user">The user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Token response</returns>
    Task<Result<TokenResponse>> GenerateTokenAsync(User user, CancellationToken cancellationToken = default);
}

/// <summary>
/// Token response containing access and refresh tokens
/// </summary>
/// <param name="AccessToken">The access token</param>
/// <param name="RefreshToken">The refresh token</param>
/// <param name="ExpiresIn">The expiration time in seconds</param>
public record TokenResponse(string AccessToken, string RefreshToken, long ExpiresIn); 