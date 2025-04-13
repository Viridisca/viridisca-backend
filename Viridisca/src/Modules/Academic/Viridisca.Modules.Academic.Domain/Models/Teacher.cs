using System;
using System.Collections.Generic;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Models
{
    /// <summary>
    /// Преподаватель
    /// </summary>
    public class Teacher : Entity
    {
        public Guid Uid { get; private set; }
        public Guid UserUid { get; private set; }
        public string EmployeeCode { get; private set; }
        public DateTime HireDate { get; private set; }
        public TeacherStatus Status { get; private set; }
        public string Specialization { get; private set; }
        public string Qualifications { get; private set; }
        public int YearsOfExperience { get; private set; }
        public string Biography { get; private set; }
        public Guid? DepartmentUid { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? LastModifiedAtUtc { get; private set; }

        // Отношения
        private readonly List<TeacherSubject> _subjects = new();
        public IReadOnlyCollection<TeacherSubject> Subjects => _subjects.AsReadOnly();

        private readonly List<TeacherGroup> _groups = new();
        public IReadOnlyCollection<TeacherGroup> Groups => _groups.AsReadOnly();

        private Teacher() { }

        public static Result<Teacher> Create(
            Guid userUid,
            string employeeCode,
            DateTime hireDate,
            string specialization,
            string qualifications,
            int yearsOfExperience,
            Guid? departmentUid = null)
        {
            if (userUid == Guid.Empty)
                return Result.Failure<Teacher>(new Error("UserUid.Empty", "User ID не может быть пустым", ErrorType.Validation));

            if (string.IsNullOrWhiteSpace(employeeCode))
                return Result.Failure<Teacher>(new Error("EmployeeCode.Empty", "Код сотрудника не может быть пустым", ErrorType.Validation));

            var teacher = new Teacher
            {
                Uid = Guid.NewGuid(),
                UserUid = userUid,
                EmployeeCode = employeeCode,
                HireDate = hireDate,
                Status = TeacherStatus.Active,
                Specialization = specialization,
                Qualifications = qualifications,
                YearsOfExperience = yearsOfExperience,
                DepartmentUid = departmentUid,
                CreatedAtUtc = DateTime.UtcNow
            };

            teacher.Raise(new TeacherCreatedDomainEvent(teacher.Uid));
            return teacher;
        }

        public void UpdateInfo(
            string specialization,
            string qualifications,
            int yearsOfExperience,
            string biography)
        {
            Specialization = specialization;
            Qualifications = qualifications;
            YearsOfExperience = yearsOfExperience;
            Biography = biography;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void AssignToDepartment(Guid departmentUid)
        {
            DepartmentUid = departmentUid;
            LastModifiedAtUtc = DateTime.UtcNow;
            Raise(new TeacherAssignedToDepartmentDomainEvent(Uid, departmentUid));
        }

        public void SetStatus(TeacherStatus status)
        {
            Status = status;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void AddSubject(TeacherSubject subject)
        {
            _subjects.Add(subject);
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void RemoveSubject(TeacherSubject subject)
        {
            _subjects.Remove(subject);
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void AddGroup(TeacherGroup group)
        {
            _groups.Add(group);
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void RemoveGroup(TeacherGroup group)
        {
            _groups.Remove(group);
            LastModifiedAtUtc = DateTime.UtcNow;
        }
    }

    public enum TeacherStatus
    {
        Active,      // Активный
        OnLeave,     // В отпуске
        Suspended,   // Приостановлен
        Terminated   // Уволен
    }
} 