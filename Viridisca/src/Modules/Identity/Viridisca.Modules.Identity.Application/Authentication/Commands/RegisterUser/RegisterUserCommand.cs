using System;
using System.Collections.Generic;
using MediatR;
using Viridisca.Modules.Identity.Domain.Models;

namespace Viridisca.Modules.Identity.Application.Authentication.Commands.RegisterUser;

public record RegisterUserCommand(
    string Email,
    string Username,
    string Password,
    string FirstName,
    string LastName,
    string MiddleName,
    string PhoneNumber,
    DateTime DateOfBirth,
    // Коллекция ролей для назначения пользователю после регистрации
    // По умолчанию - роль студента
    List<RoleType> Roles = null) : IRequest<RegisterUserResult>;

public record RegisterUserResult(Guid UserId, string Email, string Username);