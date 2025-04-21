using System;
using MediatR;

namespace Viridisca.Modules.Identity.Application.Roles.Commands;

public record AssignRoleCommand(Guid UserId, string RoleName, Guid? ScopeId = null) : IRequest; 