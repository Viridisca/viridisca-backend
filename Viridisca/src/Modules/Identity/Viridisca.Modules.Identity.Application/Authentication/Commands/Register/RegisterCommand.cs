using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Viridisca.Modules.Identity.Application.Authentication.Commands.Register;

/// <summary>
/// Command for user registration
/// </summary>
/// <param name="Email">The user's email</param>
/// <param name="Password">The user's password</param>
/// <param name="FirstName">The user's first name</param>
/// <param name="LastName">The user's last name</param>
public record RegisterCommand(
    [Required] string Email,
    [Required] string Password,
    [Required] string FirstName,
    [Required] string LastName) : IRequest<RegisterResult>;

/// <summary>
/// Result of user registration
/// </summary>
/// <param name="UserId">The ID of the registered user</param>
/// <param name="Email">The email of the registered user</param>
public record RegisterResult(Guid UserId, string Email); 