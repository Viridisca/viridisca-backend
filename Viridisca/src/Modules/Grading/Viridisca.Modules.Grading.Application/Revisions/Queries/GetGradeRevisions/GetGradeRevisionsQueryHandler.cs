using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Viridisca.Modules.Grading.Application.Revisions.Queries.GetGradeRevisions.DTO;
using Viridisca.Modules.Grading.Application.Services;
using Viridisca.Modules.Grading.Domain.Models;

namespace Viridisca.Modules.Grading.Application.Revisions.Queries.GetGradeRevisions
{
    public class GetGradeRevisionsQueryHandler : IRequestHandler<GetGradeRevisionsQuery, IReadOnlyList<GradeRevisionDto>>
    {
        private readonly GradeRevisionsService _gradeRevisionsService;
        
        public GetGradeRevisionsQueryHandler(GradeRevisionsService gradeRevisionsService)
        {
            _gradeRevisionsService = gradeRevisionsService ?? throw new ArgumentNullException(nameof(gradeRevisionsService));
        }

        public async Task<IReadOnlyList<GradeRevisionDto>> Handle(
            GetGradeRevisionsQuery request, 
            CancellationToken cancellationToken)
        {
            IReadOnlyList<GradeRevision> revisions = await _gradeRevisionsService.GetRevisionsForGradeAsync(
                request.GradeUid, 
                cancellationToken);

            return revisions
                .Select(MapToDto)
                .ToList();
        }

        private static GradeRevisionDto MapToDto(GradeRevision revision)
        {
            return new GradeRevisionDto
            {
                Uid = revision.Uid,
                GradeUid = revision.GradeUid,
                TeacherUid = revision.TeacherUid,
                PreviousValue = revision.PreviousValue,
                NewValue = revision.NewValue,
                PreviousDescription = revision.PreviousDescription,
                NewDescription = revision.NewDescription,
                RevisionReason = revision.RevisionReason,
                CreatedAtUtc = revision.CreatedAtUtc,
                TeacherName = string.Empty // В реальном приложении это поле заполнялось бы из сервиса пользователей
            };
        }
    }
} 