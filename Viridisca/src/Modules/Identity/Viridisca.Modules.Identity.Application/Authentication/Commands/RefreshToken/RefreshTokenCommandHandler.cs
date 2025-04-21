using MediatR;
using Viridisca.Common.Application.Clock;
using Viridisca.Common.Application.Data;
using Viridisca.Common.Application.Exceptions;
using Viridisca.Common.Domain;
using Viridisca.Modules.Identity.Application.Authentication.Commands.Login;
using Viridisca.Modules.Identity.Domain.Repositories;
using Viridisca.Modules.Identity.Domain.Services;

namespace Viridisca.Modules.Identity.Application.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandHandler(
    IUserRepository userRepository,
    IJwtProvider jwtProvider,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork) : IRequestHandler<RefreshTokenCommand, TokenResult>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<TokenResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // Поиск пользователя по refresh токену
        var users = await _userRepository.GetUsersByRefreshTokenAsync(request.RefreshToken, cancellationToken);

        var user = users.FirstOrDefault()
            ?? throw new ViridiscaException("RefreshToken", new Error("Authentication.InvalidToken", "Недействительный токен обновления", ErrorType.Validation));

        // Найти токен
        var refreshToken = user.RefreshTokens.FirstOrDefault(t => t.Token == request.RefreshToken);

        // Проверить активность токена
        if (refreshToken == null || !refreshToken.IsActive)
        {
            throw new ViridiscaException(
                "RefreshToken",
                new Error("Authentication.InvalidToken", "Недействительный токен обновления", ErrorType.Validation));
        }

        // Проверить активность учетной записи
        if (!user.IsActive)
        {
            throw new ViridiscaException(
                "RefreshToken",
                new Error("Authentication.UserDeactivated", "Учетная запись деактивирована", ErrorType.Validation));
        }

        // Генерация нового JWT токена
        var tokenResponse = await _jwtProvider.GenerateTokenAsync(user, cancellationToken);

        // Создание нового refresh токена
        var newRefreshTokenResult = Domain.Models.RefreshToken.Create(
            tokenResponse.Value.RefreshToken,
            user.Uid,
            DateTime.UtcNow.AddDays(7)); // TODO: брать из настроек

        if (newRefreshTokenResult.IsSuccess)
        {
            // Отозвать старый refresh токен
            refreshToken.Revoke(
                // _dateTimeProvider.UtcNow,
                "", // TODO: добавить IP-адрес
                "Замена на новый токен",
                tokenResponse.Value.RefreshToken);

            // Добавить новый refresh токен
            user.AddRefreshToken(newRefreshTokenResult.Value);

            // Обновить время последнего входа
            user.RecordLogin();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return new TokenResult(
            tokenResponse.Value.AccessToken,
            tokenResponse.Value.RefreshToken,
            tokenResponse.Value.ExpiresIn,
            user.Username,
            user.Email);
    }
}