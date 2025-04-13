using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Groups
{
    public class GroupCompletedDomainEvent : DomainEvent
    {
        public GroupCompletedDomainEvent(Guid groupUid)
        {
            GroupUid = groupUid;
        }
        
        public Guid GroupUid { get; } 
    }
} 