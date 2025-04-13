using Viridisca.Common.Application.Data;
using Viridisca.Common.Infrastructure.EF;
using Viridisca.Modules.Grading.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Viridisca.Modules.Grading.Infrastructure.EF
{
    internal sealed class GradingDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Grade> Grades { get; set; }
        public DbSet<GradeComment> GradeComments { get; set; }
        public DbSet<GradeRevision> GradeRevisions { get; set; }

        public GradingDbContext(DbContextOptions<GradingDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schemas.Grading);
            
            modelBuilder.ApplyConfiguration(new GradeConfiguration());
            modelBuilder.ApplyConfiguration(new GradeCommentConfiguration());
            modelBuilder.ApplyConfiguration(new GradeRevisionConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
} 