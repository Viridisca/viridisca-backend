using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Students
{
    /// <summary>
    /// Связь между студентом и родителем
    /// </summary>
    public class StudentParent : Entity
    {
        public Guid Uid { get; private set; }
        public Guid StudentUid { get; private set; }
        public Guid ParentUserUid { get; private set; }
        public ParentRelationType RelationType { get; private set; }
        public bool IsEmergencyContact { get; private set; }
        public bool HasAccessToGrades { get; private set; }
        public bool HasAccessToAttendance { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? LastModifiedAtUtc { get; private set; }
        
        private StudentParent() { }
        
        public static Result<StudentParent> Create(
            Guid studentUid, 
            Guid parentUserUid, 
            ParentRelationType relationType)
        {
            var studentParent = new StudentParent
            {
                Uid = Guid.NewGuid(),
                StudentUid = studentUid,
                ParentUserUid = parentUserUid,
                RelationType = relationType,
                IsEmergencyContact = true,
                HasAccessToGrades = true,
                HasAccessToAttendance = true,
                CreatedAtUtc = DateTime.UtcNow
            };
            
            return studentParent;
        }
        
        public void UpdateAccess(bool hasAccessToGrades, bool hasAccessToAttendance)
        {
            HasAccessToGrades = hasAccessToGrades;
            HasAccessToAttendance = hasAccessToAttendance;
            LastModifiedAtUtc = DateTime.UtcNow;
        }
        
        public void SetEmergencyContact(bool isEmergencyContact)
        {
            IsEmergencyContact = isEmergencyContact;
            LastModifiedAtUtc = DateTime.UtcNow;
        }
        
        public void UpdateRelationType(ParentRelationType relationType)
        {
            RelationType = relationType;
            LastModifiedAtUtc = DateTime.UtcNow;
        }
    }
} 