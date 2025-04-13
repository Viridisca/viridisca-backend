using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Grading.Domain.Models
{
    public class GradeCreatedDomainEvent : DomainEvent
    {
        public Guid GradeUid { get; }
        
        public GradeCreatedDomainEvent(Guid gradeUid)
        {
            GradeUid = gradeUid;
        }
    }
} 