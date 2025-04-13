using System;
using System.Collections.Generic;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Grading.Domain.Models
{
    /// <summary>
    /// Оценка студента
    /// </summary>
    public class Grade : Entity
    {
        public Guid Uid { get; private set; }
        public Guid StudentUid { get; private set; }
        public Guid SubjectUid { get; private set; }
        public Guid TeacherUid { get; private set; }
        public Guid? LessonUid { get; private set; }
        public decimal Value { get; private set; }
        public string Description { get; private set; }
        public GradeType Type { get; private set; }
        public DateTime IssuedAtUtc { get; private set; }
        public bool IsPublished { get; private set; }
        public DateTime? PublishedAtUtc { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? LastModifiedAtUtc { get; private set; }

        // Отношения
        private readonly List<GradeComment> _comments = new();
        public IReadOnlyCollection<GradeComment> Comments => _comments.AsReadOnly();

        private readonly List<GradeRevision> _revisions = new();
        public IReadOnlyCollection<GradeRevision> Revisions => _revisions.AsReadOnly();

        protected Grade() { }

        public static Result<Grade> Create(
            Guid studentUid,
            Guid subjectUid,
            Guid teacherUid,
            decimal value,
            GradeType type,
            string description = "",
            Guid? lessonUid = null)
        {
            if (studentUid == Guid.Empty)
                return Result.Failure<Grade>(new Error("StudentUid.Empty", "ID студента не может быть пустым", ErrorType.Validation));
            
            if (subjectUid == Guid.Empty)
                return Result.Failure<Grade>(new Error("SubjectUid.Empty", "ID предмета не может быть пустым", ErrorType.Validation));
            
            if (teacherUid == Guid.Empty)
                return Result.Failure<Grade>(new Error("TeacherUid.Empty", "ID преподавателя не может быть пустым", ErrorType.Validation));
            
            if (value < 0 || value > 100)
                return Result.Failure<Grade>(new Error("Value.Invalid", "Значение оценки должно быть от 0 до 100", ErrorType.Validation));

            var grade = new Grade
            {
                Uid = Guid.NewGuid(),
                StudentUid = studentUid,
                SubjectUid = subjectUid,
                TeacherUid = teacherUid,
                Value = value,
                Type = type,
                Description = description,
                LessonUid = lessonUid,
                IssuedAtUtc = DateTime.UtcNow,
                IsPublished = false,
                CreatedAtUtc = DateTime.UtcNow
            };

            grade.Raise(new GradeCreatedDomainEvent(grade.Uid));
            return grade;
        }

        public void UpdateValue(decimal value, string reason = null)
        {
            if (value < 0 || value > 100)
                throw new InvalidOperationException("Значение оценки должно быть от 0 до 100");
            
            // Сохраняем предыдущее значение для истории
            var previousValue = Value;
            Value = value;
            
            // Создаем запись о пересмотре оценки
            var revision = new GradeRevision(
                Guid.NewGuid(),
                Uid,
                TeacherUid,
                previousValue,
                value,
                Description,
                Description,
                reason ?? string.Empty,
                DateTime.UtcNow
            );
            
            _revisions.Add(revision);
            LastModifiedAtUtc = DateTime.UtcNow;
            
            Raise(new GradeUpdatedDomainEvent(Uid, previousValue, value));
        }

        public void UpdateDescription(string description)
        {
            Description = description;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void Publish()
        {
            if (!IsPublished)
            {
                IsPublished = true;
                PublishedAtUtc = DateTime.UtcNow;
                LastModifiedAtUtc = DateTime.UtcNow;
                Raise(new GradePublishedDomainEvent(Uid));
            }
        }

        public void Unpublish()
        {
            if (IsPublished)
            {
                IsPublished = false;
                PublishedAtUtc = null;
                LastModifiedAtUtc = DateTime.UtcNow;
            }
        }

        public void AddComment(GradeComment comment)
        {
            _comments.Add(comment);
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void RemoveComment(GradeComment comment)
        {
            _comments.Remove(comment);
            LastModifiedAtUtc = DateTime.UtcNow;
        }
    }

    public enum GradeType
    {
        Homework,     // Домашнее задание
        Quiz,         // Опрос/тест
        Test,         // Контрольная работа
        Exam,         // Экзамен
        Project,      // Проект
        Participation,// Участие
        FinalGrade,   // Итоговая оценка
        Other         // Другое
    }
} 