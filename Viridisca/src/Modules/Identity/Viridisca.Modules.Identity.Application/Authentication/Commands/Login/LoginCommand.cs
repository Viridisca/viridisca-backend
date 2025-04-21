using System.ComponentModel.DataAnnotations;
using MediatR;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Identity.Application.Authentication.Commands.Login;

/// <summary>
/// Command for user login
/// </summary>
/// <param name="Email">The user's email</param>
/// <param name="Password">The user's password</param>
public record LoginCommand([Required] string Email, [Required] string Password) : IRequest<Result<TokenResult>>
{
    /// <summary>
    /// Gets the email or username for login
    /// </summary>
    public string EmailOrUsername => Email;
}

public record TokenResult(
    string AccessToken,
    string RefreshToken,
    long ExpiresIn,
    string Username,
    string Email);