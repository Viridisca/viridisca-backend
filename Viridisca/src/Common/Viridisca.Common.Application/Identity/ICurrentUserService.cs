namespace Viridisca.Common.Application.Identity;

public interface ICurrentUserService
{
    Guid? UserId { get; }
    string? Username { get; }
    bool IsAuthenticated { get; }
    IEnumerable<string> Roles { get; }
    bool IsInRole(string role);
}