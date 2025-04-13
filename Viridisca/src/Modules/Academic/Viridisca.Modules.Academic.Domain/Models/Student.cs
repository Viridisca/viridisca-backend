using System;
using System.Collections.Generic;
using System.Linq;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Models
{
    /// <summary>
    /// Студент
    /// </summary>
    public class Student : Entity
    {
        public Guid Uid { get; private set; }
        public Guid UserUid { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string MiddleName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Guid? GroupUid { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? LastModifiedAtUtc { get; private set; }
        public string StudentCode { get; private set; }
        public DateTime EnrollmentDate { get; private set; }
        public StudentStatus Status { get; private set; }
        public string EmergencyContactName { get; private set; }
        public string EmergencyContactPhone { get; private set; }
        public string MedicalInformation { get; private set; }
        public DateTime? GraduationDate { get; private set; }

        // Отношения
        private readonly List<StudentParent> _parents = new();
        public IReadOnlyCollection<StudentParent> Parents => _parents.AsReadOnly();

        private Student() { }

        public static Result<Student> Create(
            Guid userUid, 
            string firstName, 
            string lastName, 
            string email,
            DateTime birthDate,
            string middleName = null,
            string phoneNumber = null,
            Guid? groupUid = null)
        {
            if (userUid == Guid.Empty)
                return Result.Failure<Student>(new Error("UserUid.Empty", "ID пользователя не может быть пустым", ErrorType.Validation));
            
            if (string.IsNullOrWhiteSpace(firstName))
                return Result.Failure<Student>(new Error("FirstName.Empty", "Имя не может быть пустым", ErrorType.Validation));
            
            if (string.IsNullOrWhiteSpace(lastName))
                return Result.Failure<Student>(new Error("LastName.Empty", "Фамилия не может быть пустой", ErrorType.Validation));
            
            if (string.IsNullOrWhiteSpace(email))
                return Result.Failure<Student>(new Error("Email.Empty", "Email не может быть пустым", ErrorType.Validation));

            var student = new Student
            {
                Uid = Guid.NewGuid(),
                UserUid = userUid,
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                Email = email,
                PhoneNumber = phoneNumber,
                BirthDate = birthDate,
                GroupUid = groupUid,
                IsActive = true,
                CreatedAtUtc = DateTime.UtcNow,
                EnrollmentDate = DateTime.UtcNow,
                Status = StudentStatus.Active,
                StudentCode = GenerateStudentCode()
            };

            student.Raise(new StudentCreatedDomainEvent(student.Uid));
            return student;
        }

        private static string GenerateStudentCode()
        {
            return $"S{DateTime.UtcNow:yyyyMMdd}{Guid.NewGuid().ToString().Substring(0, 4)}";
        }

        public void Update(
            string firstName,
            string lastName,
            string middleName,
            string email,
            string phoneNumber,
            DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Email = email;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void UpdateEmergencyContact(string name, string phone)
        {
            EmergencyContactName = name;
            EmergencyContactPhone = phone;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void UpdateMedicalInformation(string medicalInfo)
        {
            MedicalInformation = medicalInfo;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void UpdateStudentCode(string studentCode)
        {
            if (string.IsNullOrWhiteSpace(studentCode))
                throw new ArgumentException("Код студента не может быть пустым", nameof(studentCode));
                
            StudentCode = studentCode;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void UpdateEnrollmentDate(DateTime enrollmentDate)
        {
            EnrollmentDate = enrollmentDate;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void AssignToGroup(Guid groupUid)
        {
            if (groupUid == Guid.Empty)
                throw new ArgumentException("Group UID cannot be empty", nameof(groupUid));

            GroupUid = groupUid;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void RemoveFromGroup()
        {
            GroupUid = null;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            Status = StudentStatus.Inactive;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void Activate()
        {
            IsActive = true;
            Status = StudentStatus.Active;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void Graduate(DateTime graduationDate)
        {
            Status = StudentStatus.Graduated;
            GraduationDate = graduationDate;
            LastModifiedAtUtc = DateTime.UtcNow;
            
            Raise(new StudentGraduatedDomainEvent(Uid));
        }

        public void AddParent(StudentParent parent)
        {
            _parents.Add(parent);
            LastModifiedAtUtc = DateTime.UtcNow;
        }
    }

    public enum StudentStatus
    {
        Active,
        Inactive,
        Suspended,
        Graduated,
        Transferred,
        Expelled
    }
} 