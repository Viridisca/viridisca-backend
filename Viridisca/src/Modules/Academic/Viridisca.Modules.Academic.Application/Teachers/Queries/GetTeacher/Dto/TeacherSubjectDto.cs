using System;

namespace Viridisca.Modules.Academic.Application.Teachers.Queries.GetTeacher.Dto
{
    public class TeacherSubjectDto
    {
        public Guid Uid { get; set; }
        public Guid SubjectUid { get; set; }
        public string SubjectName { get; set; }
        public string SubjectCode { get; set; }
        public bool IsMainTeacher { get; set; }
        public DateTime AssignedAtUtc { get; set; }
        public DateTime? EndedAtUtc { get; set; }
        public bool IsActive { get; set; }
    }
} 