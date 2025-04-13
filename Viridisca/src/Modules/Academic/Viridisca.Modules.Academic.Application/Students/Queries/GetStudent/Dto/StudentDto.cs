using System;
using System.Collections.Generic;

namespace Viridisca.Modules.Academic.Application.Students.Queries.GetStudent.Dto
{
    public class StudentDto
    {
        public Guid Uid { get; set; }
        public Guid UserUid { get; set; }
        public string StudentCode { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Status { get; set; }
        public Guid? GroupUid { get; set; }
        public string GroupName { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string MedicalInformation { get; set; }
        public DateTime? GraduationDate { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public IEnumerable<StudentParentDto> Parents { get; set; }
        
        // Дополнительная информация о студенте из модуля Identity
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName => $"{LastName} {FirstName} {MiddleName}".TrimEnd();
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
} 