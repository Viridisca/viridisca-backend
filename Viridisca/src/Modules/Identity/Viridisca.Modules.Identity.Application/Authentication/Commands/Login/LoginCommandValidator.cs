using FluentValidation;

namespace Viridisca.Modules.Identity.Application.Authentication.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.EmailOrUsername)
            .NotEmpty().WithMessage("Email или имя пользователя не может быть пустым")
            .MaximumLength(256).WithMessage("Email или имя пользователя не может быть длиннее 256 символов");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Пароль не может быть пустым");
    }
} 