using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Identity.Domain.Models;

/// <summary>
/// Represents a relationship between a user and a role
/// </summary>
public class UserRole : Entity
{
    /// <summary>
    /// Gets or sets the unique identifier
    /// </summary>
    public Guid Uid { get; private set; }

    /// <summary>
    /// Gets or sets the user ID
    /// </summary>
    public Guid UserUid { get; private set; }

    /// <summary>
    /// Gets or sets the role ID
    /// </summary>
    public Guid RoleUid { get; private set; }

    /// <summary>
    /// Gets or sets the assignment date
    /// </summary>
    public DateTime AssignedAtUtc { get; private set; }

    /// <summary>
    /// Gets or sets the ID of the user who assigned this role
    /// </summary>
    public Guid? AssignedByUserUid { get; private set; }

    /// <summary>
    /// Gets or sets the scope UID for role restrictions
    /// </summary>
    public Guid? ScopeUid { get; private set; }

    /// <summary>
    /// Gets or sets whether the role is active
    /// </summary>
    public bool IsActive { get; private set; }

    /// <summary>
    /// Gets or sets the expiration date of the role
    /// </summary>
    public DateTime? ExpiresAtUtc { get; private set; }

    /// <summary>
    /// Gets or sets the user
    /// </summary>
    public virtual User User { get; private set; }

    /// <summary>
    /// Gets or sets the role
    /// </summary>
    public virtual Role Role { get; private set; }

    /// <summary>
    /// Protected constructor for EF Core
    /// </summary>
    protected UserRole()
    {
    }

    /// <summary>
    /// Private constructor for factory method
    /// </summary>
    private UserRole(Guid uid, Guid userId, Guid roleId, Guid? assignedByUserUid = null, Guid? scopeUid = null, DateTime? expiresAtUtc = null)
    {
        Uid = uid;
        UserUid = userId;
        RoleUid = roleId;
        AssignedAtUtc = DateTime.UtcNow;
        AssignedByUserUid = assignedByUserUid;
        ScopeUid = scopeUid;
        IsActive = true;
        ExpiresAtUtc = expiresAtUtc;
    }

    /// <summary>
    /// Creates a new user role
    /// </summary>
    public static Result<UserRole> Create(Guid userId, Guid roleId, Guid? assignedByUserUid = null, Guid? scopeUid = null, DateTime? expiresAtUtc = null)
    {
        if (userId == Guid.Empty)
        {
            return Result.Failure<UserRole>(Error.Failure("UserRole.InvalidUserId", "User ID cannot be empty"));
        }

        if (roleId == Guid.Empty)
        {
            return Result.Failure<UserRole>(Error.Failure("UserRole.InvalidRoleId", "Role ID cannot be empty"));
        }

        if (expiresAtUtc.HasValue && expiresAtUtc.Value <= DateTime.UtcNow)
        {
            return Result.Failure<UserRole>(Error.Failure("UserRole.InvalidExpiryDate", "Expiry date must be in the future"));
        }

        return Result.Success(new UserRole(Guid.NewGuid(), userId, roleId, assignedByUserUid, scopeUid, expiresAtUtc));
    }
}
