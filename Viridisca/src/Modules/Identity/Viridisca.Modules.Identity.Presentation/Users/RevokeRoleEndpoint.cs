using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Viridisca.Common.Presentation.Endpoints;
using Viridisca.Common.Presentation;
using Viridisca.Modules.Identity.Application.Roles.Commands;

namespace Viridisca.Modules.Identity.Presentation.Users;

internal sealed class RevokeRoleEndpoint : IEndpoint
{
    private const string AdminRoles = "SystemAdmin,SchoolDirector";
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/identity/roles/revoke", async (RevokeRoleCommand command, ISender sender) =>
        {
            await sender.Send(command);
            return Results.Ok();
        })
        .WithTags(Tags.Roles)
        .WithName("revoke-role")
        .RequireAuthorization(policy => policy.RequireRole(AdminRoles.Split(',')))
        .Produces(StatusCodes.Status200OK)
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status403Forbidden)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
} 