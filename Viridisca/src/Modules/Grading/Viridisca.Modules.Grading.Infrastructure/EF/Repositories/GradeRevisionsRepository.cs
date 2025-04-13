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
    internal sealed class GradeRevisionsRepository : IGradeRevisionsRepository
    {
        private readonly GradingDbContext _dbContext;

        public GradeRevisionsRepository(GradingDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IReadOnlyList<GradeRevision>> GetRevisionsForGradeAsync(Guid gradeUid, CancellationToken cancellationToken = default)
        {
            return await _dbContext.GradeRevisions
                .Where(r => r.GradeUid == gradeUid)
                .OrderByDescending(r => r.CreatedAtUtc)
                .ToListAsync(cancellationToken);
        }

        public async Task AddRevisionAsync(GradeRevision revision, CancellationToken cancellationToken = default)
        {
            await _dbContext.GradeRevisions.AddAsync(revision, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<GradeRevision> GetLatestRevisionForGradeAsync(Guid gradeUid, CancellationToken cancellationToken = default)
        {
            return await _dbContext.GradeRevisions
                .Where(r => r.GradeUid == gradeUid)
                .OrderByDescending(r => r.CreatedAtUtc)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
} 