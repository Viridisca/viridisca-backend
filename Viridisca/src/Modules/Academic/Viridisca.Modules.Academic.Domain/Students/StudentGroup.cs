using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Students
{
    /// <summary>
    /// Связь между студентом и группой
    /// </summary>
    public class StudentGroup : Entity
    {
        public Guid Uid { get; private set; }
        public Guid StudentUid { get; private set; }
        public Guid GroupUid { get; private set; }
        public DateTime JoinDate { get; private set; }
        public DateTime? LeaveDate { get; private set; }
        public bool IsActive => !LeaveDate.HasValue;
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? LastModifiedAtUtc { get; private set; }
        
        private StudentGroup() { }
        
        public static Result<StudentGroup> Create(Guid studentUid, Guid groupUid, DateTime joinDate)
        {
            var studentGroup = new StudentGroup
            {
                Uid = Guid.NewGuid(),
                StudentUid = studentUid,
                GroupUid = groupUid,
                JoinDate = joinDate,
                CreatedAtUtc = DateTime.UtcNow
            };
            
            return studentGroup;
        }
        
        public void SetLeaveDate(DateTime leaveDate)
        {
            if (leaveDate < JoinDate)
            {
                throw new ArgumentException("Дата выхода из группы не может быть раньше даты входа");
            }
            
            LeaveDate = leaveDate;
            LastModifiedAtUtc = DateTime.UtcNow;
        }
    }
} 