using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Models
{
    /// <summary>
    /// Связь преподавателя с группой
    /// </summary>
    public class TeacherGroup : Entity
    {
        public Guid Uid { get; private set; }
        public Guid TeacherUid { get; private set; }
        public Guid GroupUid { get; private set; }
        public Guid SubjectUid { get; private set; }
        public bool IsCurator { get; private set; }
        public DateTime AssignedAtUtc { get; private set; }
        public DateTime? EndedAtUtc { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? LastModifiedAtUtc { get; private set; }

        private TeacherGroup() { }

        public static TeacherGroup Create(
            Guid teacherUid,
            Guid groupUid,
            Guid subjectUid,
            bool isCurator = false)
        {
            return new TeacherGroup
            {
                Uid = Guid.NewGuid(),
                TeacherUid = teacherUid,
                GroupUid = groupUid,
                SubjectUid = subjectUid,
                IsCurator = isCurator,
                AssignedAtUtc = DateTime.UtcNow,
                IsActive = true,
                CreatedAtUtc = DateTime.UtcNow
            };
        }

        public void SetAsCurator(bool isCurator)
        {
            IsCurator = isCurator;
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