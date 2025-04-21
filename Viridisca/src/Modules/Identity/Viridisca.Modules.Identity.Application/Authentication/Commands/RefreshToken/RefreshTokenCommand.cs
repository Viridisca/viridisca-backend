using MediatR;
using Viridisca.Modules.Identity.Application.Authentication.Commands.Login;

namespace Viridisca.Modules.Identity.Application.Authentication.Commands.RefreshToken;

public record RefreshTokenCommand(string RefreshToken) : IRequest<TokenResult>; 