using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Viridisca.Modules.Grading.Application.Services;
using Viridisca.Modules.Grading.Domain.Models;
using Viridisca.Modules.Grading.Domain.Repositories;

namespace Viridisca.Modules.Grading.Application.Revisions.Commands.CreateGradeRevision
{
    public class CreateGradeRevisionCommandHandler : IRequestHandler<CreateGradeRevisionCommand>
    {
        private readonly GradeRevisionsService _gradeRevisionsService;
        private readonly IGradeRepository _gradeRepository;

        public CreateGradeRevisionCommandHandler(
            GradeRevisionsService gradeRevisionsService,
            IGradeRepository gradeRepository)
        {
            _gradeRevisionsService = gradeRevisionsService ?? throw new ArgumentNullException(nameof(gradeRevisionsService));
            _gradeRepository = gradeRepository ?? throw new ArgumentNullException(nameof(gradeRepository));
        }

        public async Task Handle(CreateGradeRevisionCommand request, CancellationToken cancellationToken)
        {
            // Получаем оценку
            var grade = await _gradeRepository.GetByUidAsync(request.GradeUid, cancellationToken);
            if (grade == null)
            {
                throw new InvalidOperationException($"Grade with UID {request.GradeUid} not found");
            }

            // Создаем ревизию
            await _gradeRevisionsService.CreateRevisionAsync(
                grade,
                request.PreviousValue,
                request.PreviousDescription,
                request.NewValue,
                request.NewDescription,
                request.TeacherUid,
                request.RevisionReason,
                cancellationToken);
        }
    }
} 