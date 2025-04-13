using System;

namespace Viridisca.Modules.Grading.Application.Revisions.Queries.GetGradeRevisions.DTO
{
    public class GradeRevisionDto
    {
        public Guid Uid { get; set; }
        public Guid GradeUid { get; set; }
        public Guid TeacherUid { get; set; }
        public decimal PreviousValue { get; set; }
        public decimal NewValue { get; set; }
        public string PreviousDescription { get; set; }
        public string NewDescription { get; set; }
        public string RevisionReason { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        
        // Дополнительные свойства для UI
        public string TeacherName { get; set; }
    }
} 