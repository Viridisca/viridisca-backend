using System;
using System.Collections.Generic;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Identity.Domain.Models
{
    /// <summary>
    /// Пользователь системы
    /// </summary>
    public class User : Entity
    {
        public Guid Uid { get; private set; }
        public string Email { get; private set; }
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string MiddleName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string ProfileImageUrl { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string SecurityStamp { get; private set; }
        public bool IsEmailConfirmed { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? LastLoginAtUtc { get; private set; }
        public DateTime? LastModifiedAtUtc { get; private set; }

        // Отношения
        private readonly List<UserRole> _userRoles = new();
        public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

        private readonly List<RefreshToken> _refreshTokens = new();
        public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

        private User() { }

        public static Result<User> Create(
            string email, 
            string username, 
            string passwordHash, 
            string firstName, 
            string lastName, 
            string phoneNumber, 
            DateTime dateOfBirth)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(email))
                return Result.Failure<User>(new Error("Email.Empty", "Email не может быть пустым", ErrorType.Validation));

            if (string.IsNullOrWhiteSpace(username))
                return Result.Failure<User>(new Error("Username.Empty", "Имя пользователя не может быть пустым", ErrorType.Validation));

            if (string.IsNullOrWhiteSpace(passwordHash))
                return Result.Failure<User>(new Error("Password.Empty", "Пароль не может быть пустым", ErrorType.Validation));

            var user = new User
            {
                Uid = Guid.NewGuid(),
                Email = email.Normalize().Trim().ToLowerInvariant(),
                Username = username.Trim(),
                PasswordHash = passwordHash,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                DateOfBirth = dateOfBirth,
                SecurityStamp = Guid.NewGuid().ToString(),
                IsActive = true,
                IsEmailConfirmed = false,
                CreatedAtUtc = DateTime.UtcNow
            };

            user.Raise(new UserCreatedDomainEvent(user.Uid));
            return user;
        }

        public void UpdatePersonalInfo(string firstName, string lastName, string middleName, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            PhoneNumber = phoneNumber;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void UpdateEmail(string email)
        {
            if (Email != email.Normalize().Trim().ToLowerInvariant())
            {
                Email = email.Normalize().Trim().ToLowerInvariant();
                IsEmailConfirmed = false;
                SecurityStamp = Guid.NewGuid().ToString();
                LastModifiedAtUtc = DateTime.UtcNow;
            }
        }

        public void ConfirmEmail()
        {
            IsEmailConfirmed = true;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void UpdatePassword(string passwordHash)
        {
            PasswordHash = passwordHash;
            SecurityStamp = Guid.NewGuid().ToString();
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void UpdateProfileImage(string profileImageUrl)
        {
            ProfileImageUrl = profileImageUrl;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void Activate()
        {
            IsActive = true;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void RecordLogin()
        {
            LastLoginAtUtc = DateTime.UtcNow;
        }

        public void AddRole(UserRole role)
        {
            _userRoles.Add(role);
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void RemoveRole(UserRole role)
        {
            _userRoles.Remove(role);
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void AddRefreshToken(RefreshToken token)
        {
            _refreshTokens.Add(token);
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void RemoveRefreshToken(RefreshToken token)
        {
            _refreshTokens.Remove(token);
            LastModifiedAtUtc = DateTime.UtcNow;
        }
    }
} 