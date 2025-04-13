using System;
using MediatR;

namespace Viridisca.Modules.Academic.Application.Students.Commands.AssignStudentToGroup
{
    public sealed record AssignStudentToGroupCommand(
        Guid StudentUid,
        Guid GroupUid) : IRequest<bool>;
} 