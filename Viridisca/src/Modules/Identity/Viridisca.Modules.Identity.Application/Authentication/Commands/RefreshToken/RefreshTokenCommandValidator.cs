using FluentValidation;

namespace Viridisca.Modules.Identity.Application.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Токен обновления не может быть пустым");
    }
} 