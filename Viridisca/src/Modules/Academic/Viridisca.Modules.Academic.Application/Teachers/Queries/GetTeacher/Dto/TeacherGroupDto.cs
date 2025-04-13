using System;

namespace Viridisca.Modules.Academic.Application.Teachers.Queries.GetTeacher.Dto
{
    public class TeacherGroupDto
    {
        public Guid Uid { get; set; }
        public Guid GroupUid { get; set; }
        public string GroupName { get; set; }
        public string GroupCode { get; set; }
        public Guid SubjectUid { get; set; }
        public string SubjectName { get; set; }
        public bool IsCurator { get; set; }
        public DateTime AssignedAtUtc { get; set; }
        public DateTime? EndedAtUtc { get; set; }
        public bool IsActive { get; set; }
    }
} 