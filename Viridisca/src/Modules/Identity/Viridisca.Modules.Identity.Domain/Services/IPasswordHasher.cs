namespace Viridisca.Modules.Identity.Domain.Services;

/// <summary>
/// Interface for password hashing service
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Hashes a password
    /// </summary>
    /// <param name="password">The password to hash</param>
    /// <returns>The hashed password</returns>
    string HashPassword(string password);

    /// <summary>
    /// Verifies a password against a hash
    /// </summary>
    /// <param name="passwordHash">The password hash</param>
    /// <param name="providedPassword">The password to verify</param>
    /// <returns>True if the password is valid, false otherwise</returns>
    bool VerifyPassword(string passwordHash, string providedPassword);
} 