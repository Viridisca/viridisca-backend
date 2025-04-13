using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Identity.Domain.Models
{
    public class RefreshToken : Entity
    {
        public Guid Uid { get; private set; }
        public Guid UserUid { get; private set; }
        public User User { get; private set; }
        public string Token { get; private set; }
        public DateTime IssuedAtUtc { get; private set; }
        public DateTime ExpiresAtUtc { get; private set; }
        public string CreatedByIp { get; private set; }
        public DateTime? RevokedAtUtc { get; private set; }
        public string RevokedByIp { get; private set; }
        public string ReplacedByToken { get; private set; }
        public string ReasonRevoked { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }

        private RefreshToken() { }

        public static Result<RefreshToken> Create(Guid userUid, TimeSpan tokenLifetime, string token = null)
        {
            if (userUid == Guid.Empty)
                return Result.Failure<RefreshToken>(new Error("UserUid.Empty", "Идентификатор пользователя не может быть пустым", ErrorType.Validation));
            
            return Result.Success(new RefreshToken
            {
                Uid = Guid.NewGuid(),
                UserUid = userUid,
                Token = token ?? Guid.NewGuid().ToString(),
                CreatedAtUtc = DateTime.UtcNow,
                IssuedAtUtc = DateTime.UtcNow,
                ExpiresAtUtc = DateTime.UtcNow.Add(tokenLifetime),
                RevokedAtUtc = null
            });
        }

        public bool IsExpired => DateTime.UtcNow >= ExpiresAtUtc;
        public bool IsRevoked => RevokedAtUtc != null;
        public bool IsActive => !IsRevoked && !IsExpired;

        public void Revoke(DateTime utcNow, string ipAddress, string reason = null, string replacementToken = null)
        {
            RevokedAtUtc = utcNow;
            RevokedByIp = ipAddress;
            ReasonRevoked = reason;
            ReplacedByToken = replacementToken;
        }
    }
} 