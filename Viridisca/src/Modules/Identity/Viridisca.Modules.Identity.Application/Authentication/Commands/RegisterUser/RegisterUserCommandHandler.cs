using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Viridisca.Common.Application.Data;
using Viridisca.Common.Application.Exceptions;
using Viridisca.Modules.Identity.Domain.Models;
using Viridisca.Modules.Identity.Application.Interfaces;
using Viridisca.Modules.Identity.Application.Common.Interfaces;
using Viridisca.Modules.Identity.Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Viridisca.Modules.Identity.Application.Authentication.Commands.RegisterUser;

public class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork) : IRequestHandler<RegisterUserCommand, RegisterUserResult>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Проверка, что пользователь с таким email не существует
        if (await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
        {
            throw new ConflictException("Пользователь с таким email уже существует");
        }

        // Проверка, что пользователь с таким username не существует
        if (await _userRepository.ExistsByUsernameAsync(request.Username, cancellationToken))
        {
            throw new ConflictException("Пользователь с таким именем пользователя уже существует");
        }

        // Хэширование пароля
        var passwordHash = _passwordHasher.HashPassword(request.Password);

        // Создание пользователя
        var userResult = User.Create(
            Guid.Empty,
            request.Email,
            // request.Username,
            request.FirstName,
            request.LastName,
            // request.PhoneNumber,
            passwordHash);
        // request.DateOfBirth);

        if (userResult.IsFailure)
        {
            throw new ValidationException(userResult.Error.Message);
        }

        var user = userResult.Value;

        // Обновление дополнительных полей
        user.UpdatePersonalInfo(
            request.FirstName,
            request.LastName,
            request.MiddleName,
            request.PhoneNumber);

        // Добавление ролей
        var rolesToAdd = request.Roles ?? [RoleType.Student];

        foreach (var roleType in rolesToAdd)
        {
            // var roleResult = UserRole.Create(user.Uid, roleType);
            // if (roleResult.IsSuccess)
            // {
            //     user.AddRole(roleResult.Value);
            // }
        }

        // Добавление пользователя в базу данных
        _userRepository.Insert(user);

        // Сохранение изменений
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new RegisterUserResult(user.Uid, user.Email, user.Username);
    }
}