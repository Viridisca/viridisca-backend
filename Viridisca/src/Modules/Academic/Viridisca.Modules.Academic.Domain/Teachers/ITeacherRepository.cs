using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Viridisca.Modules.Academic.Domain.Teachers
{
    public interface ITeacherRepository
    {
        Task<Teacher?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsByCodeAsync(string employeeCode, CancellationToken cancellationToken = default);
        Task<bool> ExistsByUserUidAsync(Guid userUid, CancellationToken cancellationToken = default);
        Task<List<Teacher>> GetBySubjectAsync(Guid subjectUid, CancellationToken cancellationToken = default);
        Task<List<Teacher>> GetByStatusAsync(TeacherStatus status, CancellationToken cancellationToken = default);
        Task<List<Teacher>> GetAllAsync(CancellationToken cancellationToken = default);
        void Insert(Teacher teacher);
        void Update(Teacher teacher);
        void Delete(Teacher teacher);
    }
} 