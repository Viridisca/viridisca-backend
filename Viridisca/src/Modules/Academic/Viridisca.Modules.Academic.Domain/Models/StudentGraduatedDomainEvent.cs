using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Models
{
    public class StudentGraduatedDomainEvent : DomainEvent
    {
        public Guid StudentUid { get; }

        public StudentGraduatedDomainEvent(Guid studentUid)
        {
            StudentUid = studentUid;
        } 
    }
} 