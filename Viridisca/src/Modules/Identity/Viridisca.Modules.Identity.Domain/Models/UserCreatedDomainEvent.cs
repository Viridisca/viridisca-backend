using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Identity.Domain.Models
{
    public class UserCreatedDomainEvent : DomainEvent
    {
        public Guid UserUid { get; }

        public UserCreatedDomainEvent(Guid userUid)
        {
            UserUid = userUid;
        }
    }
} 