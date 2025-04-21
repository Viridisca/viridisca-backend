using System;
using MediatR;

namespace Viridisca.Modules.Identity.Application.Roles.Commands;

public record RevokeRoleCommand(Guid UserId, string RoleName) : IRequest; 