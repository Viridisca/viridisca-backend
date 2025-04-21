//using MediatR;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Routing;
//using Viridisca.Common.Presentation;
//using Viridisca.Common.Presentation.Endpoints;
//using Viridisca.Modules.Identity.Application.Authentication.Commands.RegisterUser;

//namespace Viridisca.Modules.Identity.Presentation.Auth;

//internal sealed class RegisterEndpoint : IEndpoint
//{
//    public void MapEndpoint(IEndpointRouteBuilder app)
//    {
//        app.MapPost("api/identity/auth/register", async (RegisterUserCommand command, ISender sender) =>
//        {
//            var result = await sender.Send(command);
//            return Results.Created($"api/identity/users/{result.UserId}", result);
//        })
//        .WithTags(Tags.Auth)
//        .WithName("register")
//        .Produces<RegisterUserResult>(StatusCodes.Status201Created)
//        .ProducesValidationProblem()
//        .ProducesProblem(StatusCodes.Status409Conflict);
//    }
//} 