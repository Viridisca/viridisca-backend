using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Viridisca.Modules.Academic.Domain.Models
{
    public interface ISubjectRepository
    {
        Task<Subject> GetByUidAsync(Guid uid, CancellationToken cancellationToken = default);
        Task<Subject> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
        Task<IEnumerable<Subject>> GetByDepartmentUidAsync(Guid departmentUid, CancellationToken cancellationToken = default);
        Task<IEnumerable<Subject>> GetActiveSubjectsAsync(CancellationToken cancellationToken = default);
        Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
        void Insert(Subject subject);
        void Update(Subject subject);
        void Delete(Subject subject);
    }
} 