using Viridisca.Common.Domain;

namespace Viridisca.Modules.Identity.Domain.Models;

/// <summary>
/// Represents a role in the system
/// </summary>
public class Role : Entity
{
    /// <summary>
    /// Gets or sets the unique identifier of the role
    /// </summary>
    public Guid Uid { get; private set; }

    /// <summary>
    /// Gets or sets the role type
    /// </summary>
    public RoleType RoleType { get; private set; }

    /// <summary>
    /// Gets or sets the name of the role
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets or sets the description of the role
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Protected constructor for EF Core
    /// </summary>
    protected Role()
    {
    }

    /// <summary>
    /// Private constructor for factory method
    /// </summary>
    private Role(Guid uid, RoleType roleType, string name, string description)
    {
        Uid = uid;
        RoleType = roleType;
        Name = name;
        Description = description;
    }

    /// <summary>
    /// Creates a new role
    /// </summary>
    public static Result<Role> Create(RoleType roleType, string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<Role>(Error.Failure("Role.InvalidName", "Role name cannot be empty"));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            return Result.Failure<Role>(Error.Failure("Role.InvalidDescription", "Role description cannot be empty"));
        }

        return Result.Success(new Role(Guid.NewGuid(), roleType, name, description));
    }
} 