using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Groups
{
    public class GroupActivatedDomainEvent : DomainEvent
    {
        public GroupActivatedDomainEvent(Guid groupUid)
        {
            GroupUid = groupUid;
        }
        
        public Guid GroupUid { get; } 
    }
} 