using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Models
{
    /// <summary>
    /// Связь преподавателя с предметом
    /// </summary>
    public class TeacherSubject : Entity
    {
        public Guid Uid { get; private set; }
        public Guid TeacherUid { get; private set; }
        public Guid SubjectUid { get; private set; }
        public bool IsMainTeacher { get; private set; }
        public DateTime AssignedAtUtc { get; private set; }
        public DateTime? EndedAtUtc { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? LastModifiedAtUtc { get; private set; }

        private TeacherSubject() { }

        public static TeacherSubject Create(
            Guid teacherUid,
            Guid subjectUid,
            bool isMainTeacher = false)
        {
            return new TeacherSubject
            {
                Uid = Guid.NewGuid(),
                TeacherUid = teacherUid,
                SubjectUid = subjectUid,
                IsMainTeacher = isMainTeacher,
                AssignedAtUtc = DateTime.UtcNow,
                IsActive = true,
                CreatedAtUtc = DateTime.UtcNow
            };
        }

        public void SetAsMainTeacher(bool isMain)
        {
            IsMainTeacher = isMain;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void End(DateTime endDate)
        {
            EndedAtUtc = endDate;
            IsActive = false;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void Activate()
        {
            IsActive = true;
            EndedAtUtc = null;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            EndedAtUtc = DateTime.UtcNow;
            LastModifiedAtUtc = DateTime.UtcNow;
        }
    }
} 