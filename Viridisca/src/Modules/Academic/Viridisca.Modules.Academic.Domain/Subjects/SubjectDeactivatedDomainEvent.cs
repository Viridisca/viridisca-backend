using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Subjects
{
    public class SubjectDeactivatedDomainEvent : DomainEvent
    {
        public SubjectDeactivatedDomainEvent(Guid subjectUid)
        {
            SubjectUid = subjectUid;
        }
        
        public Guid SubjectUid { get; } 
    }
} 