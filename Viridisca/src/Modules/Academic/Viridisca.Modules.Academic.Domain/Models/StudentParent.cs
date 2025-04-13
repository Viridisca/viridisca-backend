using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Models
{
    /// <summary>
    /// Связь студента с родителем
    /// </summary>
    public class StudentParent : Entity
    {
        public Guid Uid { get; private set; }
        public Guid StudentUid { get; private set; }
        public Guid ParentUserUid { get; private set; }
        public ParentRelationType Relation { get; private set; }
        public bool IsPrimaryContact { get; private set; }
        public bool HasLegalGuardianship { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? LastModifiedAtUtc { get; private set; }

        private StudentParent() { }

        public static StudentParent Create(
            Guid studentUid,
            Guid parentUserUid,
            ParentRelationType relation,
            bool isPrimaryContact = false,
            bool hasLegalGuardianship = false)
        {
            return new StudentParent
            {
                Uid = Guid.NewGuid(),
                StudentUid = studentUid,
                ParentUserUid = parentUserUid,
                Relation = relation,
                IsPrimaryContact = isPrimaryContact,
                HasLegalGuardianship = hasLegalGuardianship,
                CreatedAtUtc = DateTime.UtcNow
            };
        }

        public void SetAsPrimaryContact(bool isPrimary)
        {
            IsPrimaryContact = isPrimary;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void UpdateLegalGuardianship(bool hasGuardianship)
        {
            HasLegalGuardianship = hasGuardianship;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void UpdateRelation(ParentRelationType relation)
        {
            Relation = relation;
            LastModifiedAtUtc = DateTime.UtcNow;
        }
    }

    public enum ParentRelationType
    {
        Mother,
        Father,
        Grandmother,
        Grandfather,
        Guardian,
        Other
    }
} 