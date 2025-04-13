using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Groups
{
    public class GroupCreatedDomainEvent : DomainEvent
    {
        public GroupCreatedDomainEvent(Guid groupUid)
        {
            GroupUid = groupUid;
        }
        
        public Guid GroupUid { get; } 
    }
} 