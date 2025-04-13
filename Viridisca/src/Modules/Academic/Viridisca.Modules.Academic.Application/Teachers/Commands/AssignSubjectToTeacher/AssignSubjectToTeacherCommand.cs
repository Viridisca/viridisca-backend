using System;
using MediatR;

namespace Viridisca.Modules.Academic.Application.Teachers.Commands.AssignSubjectToTeacher
{
    public sealed record AssignSubjectToTeacherCommand(
        Guid TeacherUid,
        Guid SubjectUid,
        bool IsMainTeacher = false) : IRequest<Guid>;
} 