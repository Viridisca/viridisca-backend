using System;
using System.Collections.Generic;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Students
{
    /// <summary>
    /// Студент учебного заведения
    /// </summary>
    public class Student : Entity
    {
        public Guid Uid { get; private set; }
        public string StudentCode { get; private set; }  // Уникальный код студента в системе
        public Guid UserUid { get; private set; }        // Связь с пользователем в Identity
        public DateTime EnrollmentDate { get; private set; }
        public DateTime? GraduationDate { get; private set; }
        public StudentStatus Status { get; private set; }
        public string AdditionalInfo { get; private set; }
        
        // Отношения
        private readonly List<StudentGroup> _studentGroups = new();
        public IReadOnlyCollection<StudentGroup> StudentGroups => _studentGroups.AsReadOnly();
        
        private readonly List<StudentParent> _parents = new();
        public IReadOnlyCollection<StudentParent> Parents => _parents.AsReadOnly();

        private Student() { }

        public static Result<Student> Create(
            string studentCode,
            Guid userUid,
            DateTime enrollmentDate)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(studentCode))
                return Result.Failure<Student>(StudentErrors.EmptyStudentCode);

            var student = new Student
            {
                Uid = Guid.NewGuid(),
                StudentCode = studentCode.Trim(),
                UserUid = userUid,
                EnrollmentDate = enrollmentDate,
                Status = StudentStatus.Active,
                CreatedAtUtc = DateTime.UtcNow
            };

            student.Raise(new StudentCreatedDomainEvent(student.Uid));
            return student;
        }

        public void AddToGroup(Guid groupUid, DateTime joinDate)
        {
            var studentGroup = StudentGroup.Create(Uid, groupUid, joinDate);
            if (studentGroup.IsSuccess)
            {
                _studentGroups.Add(studentGroup.Value);
                LastModifiedAtUtc = DateTime.UtcNow;
            }
        }

        public void RemoveFromGroup(Guid groupUid)
        {
            var studentGroup = _studentGroups.FirstOrDefault(sg => sg.GroupUid == groupUid);
            if (studentGroup != null)
            {
                studentGroup.SetLeaveDate(DateTime.UtcNow);
                LastModifiedAtUtc = DateTime.UtcNow;
            }
        }

        public void UpdateStatus(StudentStatus status)
        {
            Status = status;
            
            if (status == StudentStatus.Graduated && !GraduationDate.HasValue)
            {
                GraduationDate = DateTime.UtcNow;
            }
            
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void AddParent(Guid parentUserUid, ParentRelationType relationType)
        {
            var parentResult = StudentParent.Create(Uid, parentUserUid, relationType);
            if (parentResult.IsSuccess)
            {
                _parents.Add(parentResult.Value);
                LastModifiedAtUtc = DateTime.UtcNow;
            }
        }

        public void UpdateAdditionalInfo(string additionalInfo)
        {
            AdditionalInfo = additionalInfo;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? LastModifiedAtUtc { get; private set; }
    }
} 