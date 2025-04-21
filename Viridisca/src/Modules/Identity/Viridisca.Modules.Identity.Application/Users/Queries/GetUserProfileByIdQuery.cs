using MediatR;

namespace Viridisca.Modules.Identity.Application.Users.Queries;

public record GetUserProfileByIdQuery(Guid UserId) : IRequest<UserProfileResponse>; 