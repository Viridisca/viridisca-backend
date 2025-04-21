using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Viridisca.Common.Presentation;
using Viridisca.Common.Presentation.Endpoints;
using Viridisca.Modules.Identity.Application.Users.Queries;

namespace Viridisca.Modules.Identity.Presentation.Users;

internal sealed class GetCurrentUserProfileEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/identity/users/me", async (ClaimsPrincipal user, ISender sender) =>
        {
            var query = new GetUserProfileQuery(user.Identity.Name);
            var result = await sender.Send(query);
            return Results.Ok(result);
        })
        .WithTags(Tags.Users)
        .WithName("GetCurrentUserProfile")
        .RequireAuthorization()
        .Produces<UserProfileResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}

public record UserProfileResponse(
    Guid UserId,
    string Username,
    string Email,
    string FirstName,
    string LastName,
    string MiddleName,
    string PhoneNumber,
    string ProfileImageUrl,
    DateTime DateOfBirth,
    bool IsEmailConfirmed,
    DateTime LastLoginAt,
    IEnumerable<string> Roles); 