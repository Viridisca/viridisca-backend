using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Models
{
    public class TeacherAssignedToDepartmentDomainEvent : DomainEvent
    {
        public Guid TeacherUid { get; }
        public Guid DepartmentUid { get; }

        public TeacherAssignedToDepartmentDomainEvent(Guid teacherUid, Guid departmentUid)
        {
            TeacherUid = teacherUid;
            DepartmentUid = departmentUid;
        }
    }
} 