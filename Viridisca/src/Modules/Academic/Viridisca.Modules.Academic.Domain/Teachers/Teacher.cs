using System;
using System.Collections.Generic;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Teachers
{
    /// <summary>
    /// Преподаватель
    /// </summary>
    public class Teacher : Entity
    {
        public Guid Uid { get; private set; }
        public string EmployeeCode { get; private set; }  // Уникальный код преподавателя в системе
        public Guid UserUid { get; private set; }        // Связь с пользователем в Identity
        public DateTime HireDate { get; private set; }
        public DateTime? TerminationDate { get; private set; }
        public TeacherStatus Status { get; private set; }
        public string AcademicDegree { get; private set; }
        public string AcademicTitle { get; private set; }
        public string Specialization { get; private set; }
        public decimal HourlyRate { get; private set; }
        public string Bio { get; private set; }
        
        // Отношения
        private readonly List<TeacherSubject> _teacherSubjects = new();
        public IReadOnlyCollection<TeacherSubject> TeacherSubjects => _teacherSubjects.AsReadOnly();
        
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? LastModifiedAtUtc { get; private set; }
        
        private Teacher() { }
        
        public static Result<Teacher> Create(
            string employeeCode,
            Guid userUid,
            DateTime hireDate,
            string specialization,
            decimal hourlyRate,
            string academicDegree = "",
            string academicTitle = "",
            string bio = "")
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(employeeCode))
                return Result.Failure<Teacher>(TeacherErrors.EmptyEmployeeCode);

            if (hourlyRate < 0)
                return Result.Failure<Teacher>(TeacherErrors.InvalidHourlyRate);
            
            var teacher = new Teacher
            {
                Uid = Guid.NewGuid(),
                EmployeeCode = employeeCode.Trim(),
                UserUid = userUid,
                HireDate = hireDate,
                Specialization = specialization,
                HourlyRate = hourlyRate,
                AcademicDegree = academicDegree,
                AcademicTitle = academicTitle,
                Bio = bio,
                Status = TeacherStatus.Active,
                CreatedAtUtc = DateTime.UtcNow
            };
            
            teacher.Raise(new TeacherCreatedDomainEvent(teacher.Uid));
            return teacher;
        }
        
        public void AssignSubject(Guid subjectUid)
        {
            if (_teacherSubjects.Exists(ts => ts.SubjectUid == subjectUid && ts.IsActive))
                return;
                
            var teacherSubject = TeacherSubject.Create(Uid, subjectUid);
            if (teacherSubject.IsSuccess)
            {
                _teacherSubjects.Add(teacherSubject.Value);
                LastModifiedAtUtc = DateTime.UtcNow;
            }
        }
        
        public void RemoveSubject(Guid subjectUid)
        {
            var teacherSubject = _teacherSubjects.Find(ts => ts.SubjectUid == subjectUid && ts.IsActive);
            if (teacherSubject != null)
            {
                teacherSubject.Deactivate();
                LastModifiedAtUtc = DateTime.UtcNow;
            }
        }
        
        public void UpdateStatus(TeacherStatus status)
        {
            Status = status;
            
            if (status == TeacherStatus.Terminated && !TerminationDate.HasValue)
            {
                TerminationDate = DateTime.UtcNow;
            }
            
            LastModifiedAtUtc = DateTime.UtcNow;
        }
        
        public void UpdateDetails(
            string specialization,
            decimal hourlyRate,
            string academicDegree,
            string academicTitle,
            string bio)
        {
            if (hourlyRate < 0)
                throw new ArgumentException("Ставка оплаты не может быть отрицательной");
                
            Specialization = specialization;
            HourlyRate = hourlyRate;
            AcademicDegree = academicDegree;
            AcademicTitle = academicTitle;
            Bio = bio;
            LastModifiedAtUtc = DateTime.UtcNow;
        }
        
        public void SetTerminationDate(DateTime terminationDate)
        {
            if (terminationDate <= HireDate)
                throw new ArgumentException("Дата увольнения должна быть позже даты найма");
                
            TerminationDate = terminationDate;
            Status = TeacherStatus.Terminated;
            LastModifiedAtUtc = DateTime.UtcNow;
        }
    }
} 