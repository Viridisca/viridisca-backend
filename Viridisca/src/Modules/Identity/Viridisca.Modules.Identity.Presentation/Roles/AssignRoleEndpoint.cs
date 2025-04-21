using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Viridisca.Common.Presentation;
using Viridisca.Common.Presentation.Endpoints;
using Viridisca.Modules.Identity.Application.Roles.Commands;

namespace Viridisca.Modules.Identity.Presentation.Roles;

internal sealed class AssignRoleEndpoint : IEndpoint
{
    private const string AdminRoles = "SystemAdmin,SchoolDirector";
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/identity/roles/assign", async (AssignRoleCommand command, ISender sender) =>
        {
            await sender.Send(command);
            return Results.Ok();
        })
        .WithTags(Tags.Roles)
        .WithName("assign-role")
        .RequireAuthorization(policy => policy.RequireRole(AdminRoles.Split(',')))
        .Produces(StatusCodes.Status200OK)
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status403Forbidden)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
} 