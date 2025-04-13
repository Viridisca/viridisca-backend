using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Subjects
{
    public class SubjectCreatedDomainEvent : DomainEvent
    {
        public SubjectCreatedDomainEvent(Guid subjectUid)
        {
            SubjectUid = subjectUid;
        }
        
        public Guid SubjectUid { get; } 
    }
} 