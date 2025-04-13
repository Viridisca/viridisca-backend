using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Grading.Domain.Models;

/// <summary>
/// Ревизия оценки, сохраняющая историю изменений значения и комментария
/// </summary>
public class GradeRevision : Entity
{
    public Guid Uid { get; private set; }
    public Guid GradeUid { get; private set; }
    public Guid TeacherUid { get; private set; }
    public decimal PreviousValue { get; private set; }
    public decimal NewValue { get; private set; }
    public string PreviousDescription { get; private set; }
    public string NewDescription { get; private set; }
    public string RevisionReason { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    protected GradeRevision() { }

    public GradeRevision(
        Guid uid,
        Guid gradeUid,
        Guid teacherUid,
        decimal previousValue,
        decimal newValue,
        string previousDescription,
        string newDescription,
        string revisionReason,
        DateTime createdAtUtc)
    {
        if (uid == Guid.Empty)
            throw new ArgumentException("UID cannot be empty", nameof(uid));
        
        if (gradeUid == Guid.Empty)
            throw new ArgumentException("Grade UID cannot be empty", nameof(gradeUid));
        
        if (teacherUid == Guid.Empty)
            throw new ArgumentException("Teacher UID cannot be empty", nameof(teacherUid));

        Uid = uid;
        GradeUid = gradeUid;
        TeacherUid = teacherUid;
        PreviousValue = previousValue;
        NewValue = newValue;
        PreviousDescription = previousDescription ?? string.Empty;
        NewDescription = newDescription ?? string.Empty;
        RevisionReason = revisionReason ?? string.Empty;
        CreatedAtUtc = createdAtUtc;
    }
}
