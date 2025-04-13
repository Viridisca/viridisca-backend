using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Viridisca.Modules.Grading.Domain.Models;
using Viridisca.Modules.Grading.Domain.Repositories;

namespace Viridisca.Modules.Grading.Infrastructure.EF.Repositories
{
    internal sealed class GradeRepository : IGradeRepository
    {
        private readonly GradingDbContext _dbContext;

        public GradeRepository(GradingDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Grade> GetByUidAsync(Guid uid, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Grades
                .Include(g => g.Comments)
                .Include(g => g.Revisions)
                .FirstOrDefaultAsync(g => g.Uid == uid, cancellationToken);
        }

        public async Task<IReadOnlyList<Grade>> GetByStudentUidAsync(Guid studentUid, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Grades
                .Where(g => g.StudentUid == studentUid)
                .OrderByDescending(g => g.IssuedAtUtc)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Grade>> GetBySubjectUidAsync(Guid subjectUid, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Grades
                .Where(g => g.SubjectUid == subjectUid)
                .OrderByDescending(g => g.IssuedAtUtc)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Grade>> GetByLessonUidAsync(Guid lessonUid, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Grades
                .Where(g => g.LessonUid == lessonUid)
                .OrderByDescending(g => g.IssuedAtUtc)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Grade>> GetByTeacherUidAsync(Guid teacherUid, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Grades
                .Where(g => g.TeacherUid == teacherUid)
                .OrderByDescending(g => g.IssuedAtUtc)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsByUidAsync(Guid uid, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Grades
                .AnyAsync(g => g.Uid == uid, cancellationToken);
        }

        public async Task AddGradeAsync(Grade grade, CancellationToken cancellationToken = default)
        {
            await _dbContext.Grades.AddAsync(grade, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateGradeAsync(Grade grade, CancellationToken cancellationToken = default)
        {
            _dbContext.Grades.Update(grade);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
} 