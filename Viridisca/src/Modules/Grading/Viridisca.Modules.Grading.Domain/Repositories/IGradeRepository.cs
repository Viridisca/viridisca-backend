using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Viridisca.Modules.Grading.Domain.Models;

namespace Viridisca.Modules.Grading.Domain.Repositories
{
    public interface IGradeRepository
    {
        Task<Grade> GetByUidAsync(Guid uid, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Grade>> GetByStudentUidAsync(Guid studentUid, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Grade>> GetBySubjectUidAsync(Guid subjectUid, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Grade>> GetByLessonUidAsync(Guid lessonUid, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Grade>> GetByTeacherUidAsync(Guid teacherUid, CancellationToken cancellationToken = default);
        Task<bool> ExistsByUidAsync(Guid uid, CancellationToken cancellationToken = default);
        Task AddGradeAsync(Grade grade, CancellationToken cancellationToken = default);
        Task UpdateGradeAsync(Grade grade, CancellationToken cancellationToken = default);
    }
} 