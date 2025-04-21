using BC = BCrypt.Net.BCrypt;
using Viridisca.Modules.Identity.Domain.Services;

namespace Viridisca.Modules.Identity.Infrastructure.Users;

/// <summary>
/// Implementation of password hashing using BCrypt
/// </summary>
internal sealed class BCryptPasswordHasher : IPasswordHasher
{
    /// <inheritdoc />
    public string HashPassword(string password)
    {
        return BC.HashPassword(password);
    }

    /// <inheritdoc />
    public bool VerifyPassword(string passwordHash, string providedPassword)
    {
        return BC.Verify(providedPassword, passwordHash);
    }
} 