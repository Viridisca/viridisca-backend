using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Students
{
    public class StudentCreatedDomainEvent : DomainEvent
    {
        public StudentCreatedDomainEvent(Guid studentUid)
        {
            StudentUid = studentUid;
        }
        
        public Guid StudentUid { get; } 
    }
} 