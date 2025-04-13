using System;
using MediatR;
using Viridisca.Modules.Academic.Domain.Models;

namespace Viridisca.Modules.Academic.Application.Students.Commands.AddStudentParent
{
    public sealed record AddStudentParentCommand(
        Guid StudentUid,
        Guid ParentUserUid,
        ParentRelationType Relation,
        bool IsPrimaryContact = false,
        bool HasLegalGuardianship = false) : IRequest<Guid>;
} 