namespace Viridisca.Modules.Identity.Infrastructure.Authentication;

/// <summary>
/// JWT settings for authentication
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// Gets or sets the issuer of the token
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// Gets or sets the audience of the token
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    /// Gets or sets the secret key used to sign the token
    /// </summary>
    public string Secret { get; set; }

    /// <summary>
    /// Gets or sets the expiration time in minutes for the access token
    /// </summary>
    public int ExpiryMinutes { get; set; }
} 