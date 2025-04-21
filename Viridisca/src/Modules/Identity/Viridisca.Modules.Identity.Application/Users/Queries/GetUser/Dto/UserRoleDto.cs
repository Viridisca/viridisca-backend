using System;
using Viridisca.Modules.Identity.Domain.Models;

namespace Viridisca.Modules.Identity.Application.Users.Queries.GetUser.Dto;

public class UserRoleDto
{
    public Guid Uid { get; set; }
    public RoleType Role { get; set; }
    public string RoleName { get; set; }
    public DateTime AssignedAtUtc { get; set; }
    public bool IsActive { get; set; }
    public Guid? ScopeUid { get; set; }
    public string ScopeName { get; set; }
    public DateTime? ExpiresAtUtc { get; set; }
}
