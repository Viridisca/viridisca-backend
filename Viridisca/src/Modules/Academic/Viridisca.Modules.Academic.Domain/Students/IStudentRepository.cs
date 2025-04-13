using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Viridisca.Modules.Academic.Domain.Students
{
    public interface IStudentRepository
    {
        Task<Student?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsByCodeAsync(string studentCode, CancellationToken cancellationToken = default);
        Task<bool> ExistsByUserUidAsync(Guid userUid, CancellationToken cancellationToken = default);
        Task<List<Student>> GetByGroupAsync(Guid groupUid, CancellationToken cancellationToken = default);
        Task<List<Student>> GetByStatusAsync(StudentStatus status, CancellationToken cancellationToken = default);
        Task<List<Student>> GetAllAsync(CancellationToken cancellationToken = default);
        void Insert(Student student);
        void Update(Student student);
        void Delete(Student student);
    }
} 