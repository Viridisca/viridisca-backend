using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Viridisca.Common.Presentation;
using Viridisca.Common.Presentation.Endpoints;
using Viridisca.Modules.Identity.Application.Users.Queries;

namespace Viridisca.Modules.Identity.Presentation.Users;

internal sealed class GetUsersByRoleEndpoint : IEndpoint
{
    private const string AdminRoles = "SystemAdmin,SchoolDirector,AcademicAffairsHead";
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/identity/users/by-role/{role}", async (string role, ISender sender) =>
        {
            var query = new GetUsersByRoleQuery(role);
            var result = await sender.Send(query);
            return Results.Ok(result);
        })
        .WithTags(Tags.Users)
        .WithName("GetUsersByRole")
        .RequireAuthorization(policy => policy.RequireRole(AdminRoles.Split(',')))
        .Produces<UsersListResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status403Forbidden);
    }
} 