namespace Viridisca.Modules.Identity.Application.Common.Interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
    }
} 