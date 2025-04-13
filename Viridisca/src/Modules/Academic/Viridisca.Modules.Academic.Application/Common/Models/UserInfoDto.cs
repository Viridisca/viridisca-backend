using System;

namespace Viridisca.Modules.Academic.Application.Common.Models
{
    /// <summary>
    /// Транспортный объект с базовой информацией о пользователе из модуля Identity
    /// </summary>
    public class UserInfoDto
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
    }
} 