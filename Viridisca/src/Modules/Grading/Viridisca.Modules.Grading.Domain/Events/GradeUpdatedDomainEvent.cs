using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Grading.Domain.Models
{
    public class GradeUpdatedDomainEvent : DomainEvent
    {
        public Guid GradeUid { get; }
        public decimal PreviousValue { get; }
        public decimal NewValue { get; }
        
        public GradeUpdatedDomainEvent(Guid gradeUid, decimal previousValue, decimal newValue)
        {
            GradeUid = gradeUid;
            PreviousValue = previousValue;
            NewValue = newValue;
        }
    }
} 