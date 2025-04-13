using System;
using System.Collections.Generic;
using MediatR;
using Viridisca.Modules.Grading.Application.Revisions.Queries.GetGradeRevisions.DTO;

namespace Viridisca.Modules.Grading.Application.Revisions.Queries.GetGradeRevisions
{
    public record GetGradeRevisionsQuery : IRequest<IReadOnlyList<GradeRevisionDto>>
    {
        public Guid GradeUid { get; init; }
    }
} 