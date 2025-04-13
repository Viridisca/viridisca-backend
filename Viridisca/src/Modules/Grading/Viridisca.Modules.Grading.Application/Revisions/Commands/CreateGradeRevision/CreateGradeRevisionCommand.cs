using System;
using MediatR;

namespace Viridisca.Modules.Grading.Application.Revisions.Commands.CreateGradeRevision
{
    public record CreateGradeRevisionCommand : IRequest
    {
        public Guid GradeUid { get; init; }
        public Guid TeacherUid { get; init; }
        public decimal PreviousValue { get; init; }
        public decimal NewValue { get; init; }
        public string PreviousDescription { get; init; }
        public string NewDescription { get; init; }
        public string RevisionReason { get; init; }
    }
} 