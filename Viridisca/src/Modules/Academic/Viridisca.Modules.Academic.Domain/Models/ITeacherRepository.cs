using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Viridisca.Modules.Academic.Domain.Models
{
    public interface ITeacherRepository
    {
        Task<Teacher> GetByUidAsync(Guid uid, CancellationToken cancellationToken = default);
        Task<Teacher> GetByUserUidAsync(Guid userUid, CancellationToken cancellationToken = default);
        Task<Teacher> GetByEmployeeCodeAsync(string employeeCode, CancellationToken cancellationToken = default);
        Task<IEnumerable<Teacher>> GetByDepartmentUidAsync(Guid departmentUid, CancellationToken cancellationToken = default);
        Task<IEnumerable<Teacher>> GetBySubjectUidAsync(Guid subjectUid, CancellationToken cancellationToken = default);
        Task<bool> ExistsByEmployeeCodeAsync(string employeeCode, CancellationToken cancellationToken = default);
        void Insert(Teacher teacher);
        void Update(Teacher teacher);
        void Delete(Teacher teacher);
    }
} 