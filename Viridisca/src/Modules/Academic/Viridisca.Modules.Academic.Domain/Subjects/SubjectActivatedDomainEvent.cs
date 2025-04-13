using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Subjects
{
    public class SubjectActivatedDomainEvent : DomainEvent
    {
        public SubjectActivatedDomainEvent(Guid subjectUid)
        {
            SubjectUid = subjectUid;
        }
        
        public Guid SubjectUid { get; } 
    }
} 