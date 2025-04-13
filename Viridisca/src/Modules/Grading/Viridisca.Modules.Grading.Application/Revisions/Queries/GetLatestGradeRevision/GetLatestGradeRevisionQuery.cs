using System;
using MediatR;
using Viridisca.Modules.Grading.Application.Revisions.Queries.GetGradeRevisions.DTO;

namespace Viridisca.Modules.Grading.Application.Revisions.Queries.GetLatestGradeRevision
{
    public record GetLatestGradeRevisionQuery : IRequest<GradeRevisionDto?>
    {
        public Guid GradeUid { get; init; }
    }
} 