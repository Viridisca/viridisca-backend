using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Viridisca.Common.Presentation;
using Viridisca.Common.Presentation.Endpoints;
using Viridisca.Modules.Identity.Application.Common.Extensions;
using Viridisca.Modules.Identity.Domain.Models;

namespace Viridisca.Modules.Identity.Presentation.Roles;

internal sealed class GetAllRolesEndpoint : IEndpoint
{
    private const string AdminRoles = "SystemAdmin,SchoolDirector";
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/identity/roles", () =>
        {
            var roles = Enum.GetValues<RoleType>();
            var result = new List<RoleDto>();
            
            foreach (var role in roles)
            {
                string description = role.GetDescription();
                result.Add(new RoleDto(role.ToString(), description));
            }
            
            return Results.Ok(result);
        })
        .WithTags(Tags.Roles)
        .WithName("get-all-roles")
        .RequireAuthorization(policy => policy.RequireRole(AdminRoles.Split(',')))
        .Produces<IEnumerable<RoleDto>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status403Forbidden);
    }
}

public record RoleDto(string Name, string Description); 