using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Models
{
    public record StudentCreatedDomainEvent : IDomainEvent
    {
        public Guid Id { get; }
        public DateTime OccurredOnUtc { get; }
        public Guid StudentUid { get; }

        public StudentCreatedDomainEvent(Guid studentUid)
        {
            Id = Guid.NewGuid();
            StudentUid = studentUid;
            OccurredOnUtc = DateTime.UtcNow;
        }
    }
} 