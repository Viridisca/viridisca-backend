using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Models
{
    public class TeacherCreatedDomainEvent : DomainEvent
    {
        public Guid TeacherUid { get; }

        public TeacherCreatedDomainEvent(Guid teacherUid)
        {
            TeacherUid = teacherUid;
        }
    }
} 