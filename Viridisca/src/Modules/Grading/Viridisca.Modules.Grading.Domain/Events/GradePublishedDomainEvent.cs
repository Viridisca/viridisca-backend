using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Grading.Domain.Models
{
    public class GradePublishedDomainEvent : DomainEvent
    {
        public Guid GradeUid { get; }
        
        public GradePublishedDomainEvent(Guid gradeUid)
        {
            GradeUid = gradeUid;
        }
    }
} 