using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Teachers
{
    /// <summary>
    /// Связь между преподавателем и предметом
    /// </summary>
    public class TeacherSubject : Entity
    {
        public Guid Uid { get; private set; }
        public Guid TeacherUid { get; private set; }
        public Guid SubjectUid { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime AssignedDate { get; private set; }
        public DateTime? DeactivatedDate { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? LastModifiedAtUtc { get; private set; }
        
        private TeacherSubject() { }
        
        public static Result<TeacherSubject> Create(Guid teacherUid, Guid subjectUid)
        {
            var teacherSubject = new TeacherSubject
            {
                Uid = Guid.NewGuid(),
                TeacherUid = teacherUid,
                SubjectUid = subjectUid,
                IsActive = true,
                AssignedDate = DateTime.UtcNow,
                CreatedAtUtc = DateTime.UtcNow
            };
            
            return teacherSubject;
        }
        
        public void Deactivate()
        {
            if (IsActive)
            {
                IsActive = false;
                DeactivatedDate = DateTime.UtcNow;
                LastModifiedAtUtc = DateTime.UtcNow;
            }
        }
        
        public void Reactivate()
        {
            if (!IsActive)
            {
                IsActive = true;
                DeactivatedDate = null;
                LastModifiedAtUtc = DateTime.UtcNow;
            }
        }
    }
} 