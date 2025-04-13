using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Teachers
{
    public class TeacherCreatedDomainEvent : DomainEvent
    {
        public TeacherCreatedDomainEvent(Guid teacherUid)
        {
            TeacherUid = teacherUid;
        }
        
        public Guid TeacherUid { get; } 
    }
} 