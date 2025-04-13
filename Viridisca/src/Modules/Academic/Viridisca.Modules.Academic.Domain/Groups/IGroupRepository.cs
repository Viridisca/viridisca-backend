using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Viridisca.Modules.Academic.Domain.Groups
{
    public interface IGroupRepository
    {
        Task<Group?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Group?> GetByUidAsync(Guid uid, CancellationToken cancellationToken = default);
        Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
        Task<List<Group>> GetByDepartmentAsync(Guid departmentUid, CancellationToken cancellationToken = default);
        Task<List<Group>> GetByCuratorAsync(Guid curatorUid, CancellationToken cancellationToken = default);
        Task<List<Group>> GetByStatusAsync(GroupStatus status, CancellationToken cancellationToken = default);
        Task<List<Group>> GetAllAsync(CancellationToken cancellationToken = default);
        void Insert(Group group);
        void Update(Group group);
        void Delete(Group group); 
    }
} 