using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Models
{
    public class SubjectCreatedDomainEvent : DomainEvent
    {
        public Guid SubjectUid { get; }

        public SubjectCreatedDomainEvent(Guid subjectUid)
        {
            SubjectUid = subjectUid;
        }
    }
} 