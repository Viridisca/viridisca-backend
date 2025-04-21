using System;
using System.Collections.Generic;

namespace Viridisca.Modules.Identity.Application.Users.Queries.GetUser.Dto;

public class UserDto
{
    public Guid Uid { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string FullName => $"{LastName} {FirstName} {MiddleName}".TrimEnd();
    public string PhoneNumber { get; set; }
    public string ProfileImageUrl { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsActive { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? LastLoginAtUtc { get; set; }
    public IEnumerable<UserRoleDto> Roles { get; set; }
}
