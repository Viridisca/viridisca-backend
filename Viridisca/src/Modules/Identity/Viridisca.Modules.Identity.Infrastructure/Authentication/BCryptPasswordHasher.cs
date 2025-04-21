using Viridisca.Modules.Identity.Domain.Services;

namespace Viridisca.Modules.Identity.Infrastructure.Authentication;

/// <summary>
/// BCrypt implementation of password hasher
/// </summary>
internal sealed class BCryptPasswordHasher : IPasswordHasher
{
    /// <inheritdoc />
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    /// <inheritdoc />
    public bool VerifyPassword(string passwordHash, string providedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(providedPassword, passwordHash);
    }
} 