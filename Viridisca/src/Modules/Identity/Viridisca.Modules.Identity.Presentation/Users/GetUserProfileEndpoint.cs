using System;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Viridisca.Common.Presentation;
using Viridisca.Common.Presentation.Endpoints;
using Viridisca.Modules.Identity.Application.Users.Queries;

namespace Viridisca.Modules.Identity.Presentation.Users;

internal sealed class GetUserProfileEndpoint : IEndpoint
{
    private const string AdminRoles = "SystemAdmin,SchoolDirector,AcademicAffairsHead";
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/identity/users/{userId:guid}", async (Guid userId, ISender sender) =>
        {
            var query = new GetUserProfileByIdQuery(userId);
            var result = await sender.Send(query);
            return Results.Ok(result);
        })
        .WithTags(Tags.Users)
        .WithName("GetUserProfile")
        .RequireAuthorization(policy => policy.RequireRole(AdminRoles.Split(',')))
        .Produces<UserProfileResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
} 