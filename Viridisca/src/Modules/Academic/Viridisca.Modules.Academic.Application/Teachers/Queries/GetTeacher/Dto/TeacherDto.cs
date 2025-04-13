using System;
using System.Collections.Generic;

namespace Viridisca.Modules.Academic.Application.Teachers.Queries.GetTeacher.Dto
{
    public class TeacherDto
    {
        public Guid Uid { get; set; }
        public Guid UserUid { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime HireDate { get; set; }
        public string Status { get; set; }
        public string Specialization { get; set; }
        public string Qualifications { get; set; }
        public int YearsOfExperience { get; set; }
        public string Biography { get; set; }
        public Guid? DepartmentUid { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public IEnumerable<TeacherSubjectDto> Subjects { get; set; }
        public IEnumerable<TeacherGroupDto> Groups { get; set; }
        
        // Дополнительная информация о преподавателе из модуля Identity
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName => $"{LastName} {FirstName} {MiddleName}".TrimEnd();
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfileImageUrl { get; set; }
    }
} 