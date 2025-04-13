using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Identity.Domain.Models
{
    public class UserRole : Entity
    {
        public Guid Uid { get; private set; }
        public Guid UserUid { get; private set; }
        public RoleType Role { get; private set; }
        public DateTime AssignedAtUtc { get; private set; }
        public Guid? AssignedByUserUid { get; private set; }
        public Guid? ScopeUid { get; private set; } // Для ограничения прав определенной организацией/группой
        public bool IsActive { get; private set; }
        public DateTime? ExpiresAtUtc { get; private set; }

        private UserRole() { }

        public static Result<UserRole> Create(
            Guid userUid, 
            RoleType role, 
            Guid? assignedByUserUid = null,
            Guid? scopeUid = null,
            DateTime? expiresAtUtc = null)
        {
            if (userUid == Guid.Empty)
                return Result.Failure<UserRole>(new Error("UserUid.Empty", "Идентификатор пользователя не может быть пустым", ErrorType.Validation));

            var userRole = new UserRole
            {
                Uid = Guid.NewGuid(),
                UserUid = userUid,
                Role = role,
                AssignedAtUtc = DateTime.UtcNow,
                AssignedByUserUid = assignedByUserUid,
                ScopeUid = scopeUid,
                IsActive = true,
                ExpiresAtUtc = expiresAtUtc
            };

            return userRole;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void UpdateExpiration(DateTime? expiresAtUtc)
        {
            ExpiresAtUtc = expiresAtUtc;
        }

        public void UpdateScope(Guid? scopeUid)
        {
            ScopeUid = scopeUid;
        }

        public bool IsExpired()
        {
            return ExpiresAtUtc.HasValue && ExpiresAtUtc.Value < DateTime.UtcNow;
        }
    }
} 