using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Viridisca.Common.Presentation;
using Microsoft.AspNetCore.Routing;
using Viridisca.Common.Presentation.Endpoints;
using Viridisca.Modules.Identity.Application.Users.Commands;

namespace Viridisca.Modules.Identity.Presentation.Users;

internal sealed class UpdateUserProfileEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("api/identity/users/me", async (UpdateUserProfileCommand command, ISender sender) =>
        {
            await sender.Send(command);
            return Results.Ok();
        })
        .WithTags(Tags.Users)
        .WithName("update-user-profile")
        .RequireAuthorization()
        .Produces(StatusCodes.Status200OK)
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status401Unauthorized);
    }
} 