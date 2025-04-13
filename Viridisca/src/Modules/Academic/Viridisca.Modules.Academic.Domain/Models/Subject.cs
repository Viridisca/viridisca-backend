using System;
using System.Collections.Generic;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Models
{
    /// <summary>
    /// Учебный предмет
    /// </summary>
    public class Subject : Entity
    {
        public Guid Uid { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string Description { get; private set; }
        public int Credits { get; private set; }
        public SubjectType Type { get; private set; }
        public Guid? DepartmentUid { get; private set; }
        public string Syllabus { get; private set; }
        public SubjectDifficulty Difficulty { get; private set; }
        public int? MinimumRequiredGrade { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? LastModifiedAtUtc { get; private set; }

        // Отношения
        private readonly List<TeacherSubject> _teachers = new();
        public IReadOnlyCollection<TeacherSubject> Teachers => _teachers.AsReadOnly();

        private Subject() { }

        public static Result<Subject> Create(
            string name,
            string code,
            string description,
            int credits,
            SubjectType type,
            SubjectDifficulty difficulty,
            Guid? departmentUid = null,
            int? minimumRequiredGrade = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<Subject>(new Error("Name.Empty", "Название предмета не может быть пустым", ErrorType.Validation));

            if (string.IsNullOrWhiteSpace(code))
                return Result.Failure<Subject>(new Error("Code.Empty", "Код предмета не может быть пустым", ErrorType.Validation));

            var subject = new Subject
            {
                Uid = Guid.NewGuid(),
                Name = name,
                Code = code,
                Description = description,
                Credits = credits,
                Type = type,
                Difficulty = difficulty,
                DepartmentUid = departmentUid,
                MinimumRequiredGrade = minimumRequiredGrade,
                IsActive = true,
                CreatedAtUtc = DateTime.UtcNow
            };

            subject.Raise(new SubjectCreatedDomainEvent(subject.Uid));
            return subject;
        }

        public void UpdateInfo(string name, string description, int credits)
        {
            Name = name;
            Description = description;
            Credits = credits;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void UpdateSyllabus(string syllabus)
        {
            Syllabus = syllabus;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void AssignToDepartment(Guid departmentUid)
        {
            DepartmentUid = departmentUid;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void SetDifficulty(SubjectDifficulty difficulty)
        {
            Difficulty = difficulty;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void SetMinimumRequiredGrade(int minimumRequiredGrade)
        {
            MinimumRequiredGrade = minimumRequiredGrade;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void Activate()
        {
            IsActive = true;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void AddTeacher(TeacherSubject teacherSubject)
        {
            _teachers.Add(teacherSubject);
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void RemoveTeacher(TeacherSubject teacherSubject)
        {
            _teachers.Remove(teacherSubject);
            LastModifiedAtUtc = DateTime.UtcNow;
        }
    }

    public enum SubjectType
    {
        Core,        // Основной предмет
        Elective,    // Предмет по выбору
        Laboratory,  // Лабораторный
        Practical,   // Практический
        Seminar,     // Семинар
        Research,    // Исследовательский
        Workshop     // Мастер-класс
    }

    public enum SubjectDifficulty
    {
        Beginner,     // Начальный
        Intermediate, // Средний
        Advanced,     // Продвинутый
        Expert        // Экспертный
    }
} 