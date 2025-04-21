using System;
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Viridisca.Common.Application.Data;
using Viridisca.Modules.Identity.Application.Common.Interfaces;
using Viridisca.Modules.Identity.Domain.Models;
using Viridisca.Modules.Identity.Domain.Repositories;

namespace Viridisca.Modules.Identity.Application.Users.Commands.CreateUser;

internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Проверяем, что пользователя с таким email или username еще нет
        var emailExists = await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken);
        if (emailExists)
        {
            throw new Exception($"Пользователь с email {request.Email} уже существует");
        }

        var usernameExists = await _userRepository.ExistsByUsernameAsync(request.Username, cancellationToken);
        if (usernameExists)
        {
            throw new Exception($"Пользователь с именем {request.Username} уже существует");
        }

        // Хешируем пароль
        var passwordHash = _passwordHasher.HashPassword(request.Password);

        // Создаем пользователя
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
            throw new Exception(userResult.Error.Message);
        }

        var user = userResult.Value;

        // Сохраняем пользователя
        _userRepository.Insert(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Uid;
    }
}
