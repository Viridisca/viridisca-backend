using System;
using MediatR;

namespace Viridisca.Modules.Identity.Application.Users.Commands.CreateUser
{
    public sealed record CreateUserCommand(
        string Email,
        string Username,
        string Password,
        string FirstName,
        string LastName,
        string PhoneNumber,
        DateTime DateOfBirth) : IRequest<Guid>;
} 