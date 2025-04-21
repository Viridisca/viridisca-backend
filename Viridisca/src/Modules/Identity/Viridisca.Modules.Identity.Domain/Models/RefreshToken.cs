using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Identity.Domain.Models;

/// <summary>
/// Represents a refresh token used for authentication
/// </summary>
public class RefreshToken : Entity
{
    /// <summary>
    /// Gets or sets the unique identifier for the refresh token
    /// </summary>
    public Guid Uid { get; private set; }

    /// <summary>
    /// Gets or sets the token string
    /// </summary>
    public string Token { get; private set; }

    /// <summary>
    /// Gets or sets the user ID associated with this token
    /// </summary>
    public Guid UserUid { get; private set; }

    /// <summary>
    /// Gets or sets the expiry date of the token
    /// </summary>
    public DateTime ExpiryDate { get; private set; }

    /// <summary>
    /// Gets or sets the creation date of the token
    /// </summary>
    public DateTime CreatedDate { get; private set; }

    /// <summary>
    /// Gets or sets the revocation date of the token
    /// </summary>
    public DateTime? RevokedAtUtc { get; private set; }

    /// <summary>
    /// Gets or sets the IP address that revoked the token
    /// </summary>
    public string RevokedByIp { get; private set; }

    /// <summary>
    /// Gets or sets the token that replaced this token
    /// </summary>
    public string ReplacedByToken { get; private set; }

    /// <summary>
    /// Gets or sets the reason the token was revoked
    /// </summary>
    public string ReasonRevoked { get; private set; }

    /// <summary>
    /// Gets or sets the user associated with this token
    /// </summary>
    public virtual User User { get; private set; }

    /// <summary>
    /// Gets whether the token is expired
    /// </summary>
    public bool IsExpired => DateTime.UtcNow >= ExpiryDate;

    /// <summary>
    /// Gets whether the token is revoked
    /// </summary>
    public bool IsRevoked => RevokedAtUtc != null;

    /// <summary>
    /// Gets whether the token is active
    /// </summary>
    public bool IsActive => !IsRevoked && !IsExpired;

    /// <summary>
    /// Protected constructor for EF Core
    /// </summary>
    protected RefreshToken()
    {
    }

    /// <summary>
    /// Private constructor for factory method
    /// </summary>
    private RefreshToken(Guid uid, string token, Guid userId, DateTime expiryDate)
    {
        Uid = uid;
        Token = token;
        UserUid = userId;
        ExpiryDate = expiryDate;
        CreatedDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Creates a new refresh token
    /// </summary>
    public static Result<RefreshToken> Create(string token, Guid userId, DateTime expiryDate)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return Result.Failure<RefreshToken>(Error.Failure("RefreshToken.InvalidToken", "Token cannot be empty"));
        }

        if (userId == Guid.Empty)
        {
            return Result.Failure<RefreshToken>(Error.Failure("RefreshToken.InvalidUserId", "User ID cannot be empty"));
        }

        if (expiryDate <= DateTime.UtcNow)
        {
            return Result.Failure<RefreshToken>(Error.Failure("RefreshToken.InvalidExpiryDate", "Expiry date must be in the future"));
        }

        return Result.Success(new RefreshToken(Guid.NewGuid(), token, userId, expiryDate));
    }

    /// <summary>
    /// Revokes the refresh token
    /// </summary>
    public Result Revoke(string reason = null, string revokedByIp = null, string replacedByToken = null)
    {
        if (IsRevoked)
        {
            return Result.Failure(Error.Failure("RefreshToken.AlreadyRevoked", "Token is already revoked"));
        }

        RevokedAtUtc = DateTime.UtcNow;
        ReasonRevoked = reason;
        RevokedByIp = revokedByIp;
        ReplacedByToken = replacedByToken;

        return Result.Success();
    }
}
