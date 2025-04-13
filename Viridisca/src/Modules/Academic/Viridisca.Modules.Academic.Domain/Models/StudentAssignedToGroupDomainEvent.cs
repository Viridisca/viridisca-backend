using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Models
{
    public class StudentAssignedToGroupDomainEvent : DomainEvent
    {
        public Guid StudentUid { get; }
        public Guid GroupUid { get; }

        public StudentAssignedToGroupDomainEvent(Guid studentUid, Guid groupUid)
        {
            StudentUid = studentUid;
            GroupUid = groupUid;
        } 
    }
}