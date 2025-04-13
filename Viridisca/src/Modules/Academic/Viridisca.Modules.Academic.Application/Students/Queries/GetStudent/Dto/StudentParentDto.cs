using System;

namespace Viridisca.Modules.Academic.Application.Students.Queries.GetStudent.Dto
{
    public class StudentParentDto
    {
        public Guid Uid { get; set; }
        public Guid ParentUserUid { get; set; }
        public string ParentFullName { get; set; }
        public string Relation { get; set; }
        public bool IsPrimaryContact { get; set; }
        public bool HasLegalGuardianship { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
} 