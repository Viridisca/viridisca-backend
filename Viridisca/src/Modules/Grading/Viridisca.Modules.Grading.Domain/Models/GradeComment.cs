using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Grading.Domain.Models
{
    /// <summary>
    /// Комментарий к оценке, оставленный преподавателем или студентом
    /// </summary>
    public class GradeComment : Entity
    {
        public Guid Uid { get; private set; }
        public Guid GradeUid { get; private set; }
        public Guid AuthorUid { get; private set; }
        public GradeCommentType Type { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? LastModifiedAtUtc { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAtUtc { get; private set; }

        protected GradeComment() { }

        public GradeComment(
            Guid uid,
            Guid gradeUid,
            Guid authorUid,
            GradeCommentType type,
            string content)
        {
            if (uid == Guid.Empty)
                throw new ArgumentException("UID cannot be empty", nameof(uid));
            
            if (gradeUid == Guid.Empty)
                throw new ArgumentException("Grade UID cannot be empty", nameof(gradeUid));
            
            if (authorUid == Guid.Empty)
                throw new ArgumentException("Author UID cannot be empty", nameof(authorUid));
            
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Comment content cannot be empty", nameof(content));

            Uid = uid;
            GradeUid = gradeUid;
            AuthorUid = authorUid;
            Type = type;
            Content = content;
            CreatedAtUtc = DateTime.UtcNow;
            IsDeleted = false;
        }

        public void UpdateContent(string newContent)
        {
            if (string.IsNullOrWhiteSpace(newContent))
                throw new ArgumentException("Comment content cannot be empty", nameof(newContent));

            Content = newContent;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        public void Delete()
        {
            if (IsDeleted)
                return;

            IsDeleted = true;
            DeletedAtUtc = DateTime.UtcNow;
        }
    }

    public enum GradeCommentType
    {
        Teacher,  // Комментарий преподавателя
        Student,  // Комментарий студента
        System    // Системный комментарий
    }
} 