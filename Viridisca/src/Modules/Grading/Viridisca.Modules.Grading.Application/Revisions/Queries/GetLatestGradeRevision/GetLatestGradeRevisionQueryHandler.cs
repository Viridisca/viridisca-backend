using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Viridisca.Modules.Grading.Application.Revisions.Queries.GetGradeRevisions.DTO;
using Viridisca.Modules.Grading.Application.Services;
using Viridisca.Modules.Grading.Domain.Models;

namespace Viridisca.Modules.Grading.Application.Revisions.Queries.GetLatestGradeRevision
{
    public class GetLatestGradeRevisionQueryHandler : IRequestHandler<GetLatestGradeRevisionQuery, GradeRevisionDto?>
    {
        private readonly GradeRevisionsService _gradeRevisionsService;
        
        public GetLatestGradeRevisionQueryHandler(GradeRevisionsService gradeRevisionsService)
        {
            _gradeRevisionsService = gradeRevisionsService ?? throw new ArgumentNullException(nameof(gradeRevisionsService));
        }

        public async Task<GradeRevisionDto?> Handle(
            GetLatestGradeRevisionQuery request, 
            CancellationToken cancellationToken)
        {
            GradeRevision? revision = await _gradeRevisionsService.GetLatestRevisionAsync(
                request.GradeUid, 
                cancellationToken);

            if (revision == null)
                return null;

            return MapToDto(revision);
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