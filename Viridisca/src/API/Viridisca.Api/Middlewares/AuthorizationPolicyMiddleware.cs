using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Viridisca.Api.Middleware;

public class AuthorizationPolicyMiddleware(RequestDelegate next, IAuthorizationService authorizationService)
{
    private readonly RequestDelegate _next = next;
    private readonly IAuthorizationService _authorizationService = authorizationService;

    public async Task InvokeAsync(HttpContext context)
    {
        // Skip authorization for Swagger and other development endpoints
        if (context.Request.Path.StartsWithSegments("/swagger") ||
            context.Request.Path.StartsWithSegments("/health"))
        {
            await _next(context);
            return;
        }

        // Check if user is authenticated
        if (context.User.Identity?.IsAuthenticated != true)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        // Get all the roles from the user claims
        var userRoles = context.User.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList();

        // If no roles or empty roles, deny access
        if (userRoles.Count == 0)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return;
        }

        // Continue with the next middleware
        await _next(context);
    }
} 