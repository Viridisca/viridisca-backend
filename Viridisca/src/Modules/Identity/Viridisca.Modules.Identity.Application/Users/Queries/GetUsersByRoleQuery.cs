using MediatR;

namespace Viridisca.Modules.Identity.Application.Users.Queries;

public record GetUsersByRoleQuery(string Role) : IRequest<UsersListResponse>;

public record UsersListResponse(IEnumerable<UserProfileResponse> Users, int TotalCount); 