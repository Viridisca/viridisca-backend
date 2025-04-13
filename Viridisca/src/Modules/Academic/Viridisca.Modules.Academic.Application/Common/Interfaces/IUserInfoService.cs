using System;
using System.Threading;
using System.Threading.Tasks;
using Viridisca.Modules.Academic.Application.Common.Models;

namespace Viridisca.Modules.Academic.Application.Common.Interfaces
{
    /// <summary>
    /// Сервис для получения информации о пользователях из модуля Identity
    /// </summary>
    public interface IUserInfoService
    {
        Task<UserInfoDto> GetUserInfoAsync(Guid userUid, CancellationToken cancellationToken = default);
        Task<string> GetUserFullNameAsync(Guid userUid, CancellationToken cancellationToken = default);
        Task<string> GetUserEmailAsync(Guid userUid, CancellationToken cancellationToken = default);
        Task<string> GetUserPhoneAsync(Guid userUid, CancellationToken cancellationToken = default);
        Task<bool> UserExistsAsync(Guid userUid, CancellationToken cancellationToken = default);
    }
} 