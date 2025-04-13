using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Viridisca.Modules.Academic.Domain.Subjects
{
    public interface ISubjectRepository
    {
        Task<Subject?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
        Task<List<Subject>> GetByDepartmentAsync(Guid departmentUid, CancellationToken cancellationToken = default);
        Task<List<Subject>> GetByTypeAsync(SubjectType type, CancellationToken cancellationToken = default);
        Task<List<Subject>> GetActiveAsync(CancellationToken cancellationToken = default);
        Task<List<Subject>> GetAllAsync(CancellationToken cancellationToken = default);
        void Insert(Subject subject);
        void Update(Subject subject);
        void Delete(Subject subject);
    }
}