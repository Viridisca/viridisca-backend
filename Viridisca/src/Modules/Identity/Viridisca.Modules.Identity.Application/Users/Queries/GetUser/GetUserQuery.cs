using System;
using MediatR;
using Viridisca.Modules.Identity.Application.Users.Queries.GetUser.Dto;

namespace Viridisca.Modules.Identity.Application.Users.Queries.GetUser
{
    public sealed record GetUserQuery(Guid UserUid) : IRequest<UserDto>;
} 