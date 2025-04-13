using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Viridisca.Modules.Academic.Domain.Models;

namespace Viridisca.Modules.Academic.Domain.Repositories
{
    public interface IStudentRepository
    {
        Task<Student> GetByUidAsync(Guid uid, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Student>> GetByGroupUidAsync(Guid groupUid, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Student>> GetAllActiveAsync(CancellationToken cancellationToken = default);
        Task<bool> ExistsByUidAsync(Guid uid, CancellationToken cancellationToken = default);
        Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task AddAsync(Student student, CancellationToken cancellationToken = default);
        Task UpdateAsync(Student student, CancellationToken cancellationToken = default);
        Task RemoveAsync(Student student, CancellationToken cancellationToken = default);
    }
} 