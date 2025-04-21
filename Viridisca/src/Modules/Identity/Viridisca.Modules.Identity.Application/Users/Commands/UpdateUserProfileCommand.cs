using MediatR;

namespace Viridisca.Modules.Identity.Application.Users.Commands;

public record UpdateUserProfileCommand(
    string FirstName,
    string LastName,
    string MiddleName,
    string PhoneNumber,
    DateTime DateOfBirth) : IRequest; 