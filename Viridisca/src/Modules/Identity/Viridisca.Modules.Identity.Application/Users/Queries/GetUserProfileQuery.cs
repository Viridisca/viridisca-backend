using MediatR;

namespace Viridisca.Modules.Identity.Application.Users.Queries;

public record GetUserProfileQuery(string Username) : IRequest<UserProfileResponse>;

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