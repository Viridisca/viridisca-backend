using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Viridisca.Modules.Identity.Application.Users.Queries.GetUser.Dto;
using Viridisca.Modules.Identity.Domain.Models;

namespace Viridisca.Modules.Identity.Application.Users.Queries.GetUser
{
    internal sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUidAsync(request.UserUid, cancellationToken);

            if (user == null)
            {
                throw new Exception($"Пользователь с ID {request.UserUid} не найден");
            }

            return new UserDto
            {
                Uid = user.Uid,
                Email = user.Email,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                PhoneNumber = user.PhoneNumber,
                ProfileImageUrl = user.ProfileImageUrl,
                DateOfBirth = user.DateOfBirth,
                IsActive = user.IsActive,
                IsEmailConfirmed = user.IsEmailConfirmed,
                CreatedAtUtc = user.CreatedAtUtc,
                LastLoginAtUtc = user.LastLoginAtUtc,
                Roles = user.UserRoles.Select(r => new UserRoleDto
                {
                    Uid = r.Uid,
                    Role = r.Role,
                    RoleName = GetRoleDisplayName(r.Role),
                    AssignedAtUtc = r.AssignedAtUtc,
                    IsActive = r.IsActive,
                    ScopeUid = r.ScopeUid,
                    ExpiresAtUtc = r.ExpiresAtUtc
                }).ToList()
            };
        }

        private string GetRoleDisplayName(RoleType role)
        {
            // В реальном проекте лучше использовать Reflection для получения значения Description атрибута
            return role.ToString();
        }
    }
} 