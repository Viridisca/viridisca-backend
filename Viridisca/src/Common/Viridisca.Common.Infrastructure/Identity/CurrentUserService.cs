using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Viridisca.Common.Application.Identity;

namespace Viridisca.Common.Infrastructure.Identity;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public Guid? UserId
    {
        get
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return string.IsNullOrEmpty(userId) ? null : Guid.Parse(userId);
        }
    }

    public string? Username => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);

    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    public IEnumerable<string> Roles => _httpContextAccessor.HttpContext?.User?.Claims
        .Where(c => c.Type == ClaimTypes.Role)
        .Select(c => c.Value) ?? [];

    public bool IsInRole(string role)
    {
        return _httpContextAccessor.HttpContext?.User?.IsInRole(role) ?? false;
    }
}
