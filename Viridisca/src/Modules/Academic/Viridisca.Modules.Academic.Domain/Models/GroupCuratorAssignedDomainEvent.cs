using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Models
{
    public class GroupCuratorAssignedDomainEvent : DomainEvent
    {
        public Guid GroupUid { get; }
        public Guid CuratorUid { get; }

        public GroupCuratorAssignedDomainEvent(Guid groupUid, Guid curatorUid)
        {
            GroupUid = groupUid;
            CuratorUid = curatorUid;
        } 
    }
} 