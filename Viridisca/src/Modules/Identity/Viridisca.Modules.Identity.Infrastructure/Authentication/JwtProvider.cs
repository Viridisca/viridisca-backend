using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Viridisca.Common.Domain;
using Viridisca.Modules.Identity.Domain.Models;
using Viridisca.Modules.Identity.Domain.Services;

namespace Viridisca.Modules.Identity.Infrastructure.Authentication;

/// <summary>
/// JWT provider implementation
/// </summary>
internal sealed class JwtProvider : IJwtProvider
{
    private readonly JwtSettings _jwtSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtProvider"/> class.
    /// </summary>
    /// <param name="jwtSettings">The JWT settings.</param>
    public JwtProvider(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    /// <inheritdoc />
    public string GenerateAccessToken(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "User cannot be null");
        }

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Uid.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim("uid", user.Uid.ToString())
        };

        // Add role claims
        foreach (var userRole in user.UserRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole.Role.RoleType.ToString()));
        }

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    /// <inheritdoc />
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    /// <summary>
    /// Creates a token response
    /// </summary>
    /// <param name="user">The user</param>
    /// <returns>A token response</returns>
    public Result<TokenResponse> CreateTokenResponse(User user)
    {
        if (user == null)
        {
            return Result.Failure<TokenResponse>(Error.Failure("Jwt.NullUser", "User cannot be null"));
        }

        try
        {
            string accessToken = GenerateAccessToken(user);
            string refreshToken = GenerateRefreshToken();

            return Result.Success(new TokenResponse(
                accessToken,
                refreshToken,
                _jwtSettings.ExpiryMinutes * 60)); // Convert minutes to seconds
        }
        catch (Exception ex)
        {
            return Result.Failure<TokenResponse>(Error.Failure("Jwt.TokenGenerationFailed", ex.Message));
        }
    }
    
    /// <inheritdoc />
    public Task<Result<TokenResponse>> GenerateTokenAsync(User user, CancellationToken cancellationToken = default)
    {
        var result = CreateTokenResponse(user);
        return Task.FromResult(result);
    }
} 