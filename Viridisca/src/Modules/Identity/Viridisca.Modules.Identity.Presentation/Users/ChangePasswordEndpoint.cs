using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Viridisca.Common.Presentation;
using Viridisca.Common.Presentation.Endpoints;
using Viridisca.Modules.Identity.Application.Users.Commands;

namespace Viridisca.Modules.Identity.Presentation.Users;

internal sealed class ChangePasswordEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/identity/users/me/change-password", async (ChangePasswordCommand command, ISender sender) =>
        {
            await sender.Send(command);
            return Results.Ok();
        })
        .WithTags(Tags.Users)
        .WithName("ChangePassword")
        .RequireAuthorization()
        .Produces(StatusCodes.Status200OK)
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status401Unauthorized);
    }
} 