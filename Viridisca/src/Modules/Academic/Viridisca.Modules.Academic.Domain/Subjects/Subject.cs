using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Subjects
{
    /// <summary>
    /// Учебный предмет
    /// </summary>
    public class Subject : Entity
    {
        public Guid Uid { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Credits { get; private set; }
        public int LessonsPerWeek { get; private set; }
        public SubjectType Type { get; private set; }
        public bool IsActive { get; private set; }
        public Guid DepartmentUid { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? LastModifiedAtUtc { get; private set; }
        
        private Subject() { }
        
        public static Result<Subject> Create(
            string code,
            string name,
            string description,
            int credits,
            int lessonsPerWeek,
            SubjectType type,
            Guid departmentUid)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(code))
                return Result.Failure<Subject>(SubjectErrors.EmptySubjectCode);
                
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<Subject>(SubjectErrors.EmptySubjectName);
                
            if (credits <= 0)
                return Result.Failure<Subject>(SubjectErrors.InvalidCredits);
                
            if (lessonsPerWeek <= 0)
                return Result.Failure<Subject>(SubjectErrors.InvalidLessonsPerWeek);
            
            var subject = new Subject
            {
                Uid = Guid.NewGuid(),
                Code = code.Trim(),
                Name = name.Trim(),
                Description = description,
                Credits = credits,
                LessonsPerWeek = lessonsPerWeek,
                Type = type,
                IsActive = true,
                DepartmentUid = departmentUid,
                CreatedAtUtc = DateTime.UtcNow
            };
            
            subject.Raise(new SubjectCreatedDomainEvent(subject.Uid));
            return subject;
        }
        
        public void UpdateDetails(
            string name,
            string description,
            int credits,
            int lessonsPerWeek,
            SubjectType type)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название предмета не может быть пустым");
                
            if (credits <= 0)
                throw new ArgumentException("Количество кредитов должно быть положительным числом");
                
            if (lessonsPerWeek <= 0)
                throw new ArgumentException("Количество уроков в неделю должно быть положительным числом");
            
            Name = name.Trim();
            Description = description;
            Credits = credits;
            LessonsPerWeek = lessonsPerWeek;
            Type = type;
            LastModifiedAtUtc = DateTime.UtcNow;
        }
        
        public void Activate()
        {
            if (!IsActive)
            {
                IsActive = true;
                LastModifiedAtUtc = DateTime.UtcNow;
                Raise(new SubjectActivatedDomainEvent(Uid));
            }
        }
        
        public void Deactivate()
        {
            if (IsActive)
            {
                IsActive = false;
                LastModifiedAtUtc = DateTime.UtcNow;
                Raise(new SubjectDeactivatedDomainEvent(Uid));
            }
        }
    }
} 