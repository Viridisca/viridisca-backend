using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Viridisca.Modules.Identity.Domain.Models
{
    public interface IUserRepository
    {
        Task<User> GetByUidAsync(Guid uid, CancellationToken cancellationToken = default);
        Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<User> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
        Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> GetUsersByRoleAsync(RoleType role, CancellationToken cancellationToken = default);
        void Insert(User user);
        void Update(User user);
        void Delete(User user);
    }
} 