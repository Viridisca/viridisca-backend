//using MediatR;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Routing;
//using Viridisca.Common.Presentation;
//using Viridisca.Common.Presentation.Endpoints;
//using Viridisca.Modules.Identity.Application.Authentication.Commands.Login;

//namespace Viridisca.Modules.Identity.Presentation.Auth;

//internal sealed class LoginEndpoint : IEndpoint
//{
//    public void MapEndpoint(IEndpointRouteBuilder app)
//    {
//        app.MapPost("api/identity/auth/login", async (LoginCommand command, ISender sender) =>
//        {
//            var result = await sender.Send(command);
//            return Results.Ok(result);
//        })
//        .WithTags(Tags.Auth)
//        .WithName("login")
//        .Produces<TokenResult>(StatusCodes.Status200OK)
//        .ProducesValidationProblem()
//        .ProducesProblem(StatusCodes.Status401Unauthorized);
//    } 
//} 