using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Viridisca.Modules.Academic.Domain.Models
{
    public interface IStudentRepository
    {
        Task<Student> GetByUidAsync(Guid uid, CancellationToken cancellationToken = default);
        Task<Student> GetByUserUidAsync(Guid userUid, CancellationToken cancellationToken = default);
        Task<Student> GetByStudentCodeAsync(string studentCode, CancellationToken cancellationToken = default);
        Task<IEnumerable<Student>> GetByGroupUidAsync(Guid groupUid, CancellationToken cancellationToken = default);
        Task<bool> ExistsByStudentCodeAsync(string studentCode, CancellationToken cancellationToken = default);
        void Insert(Student student);
        void Update(Student student);
        void Delete(Student student);
    }
} 