using FluentValidation;

namespace Viridisca.Modules.Identity.Application.Users.Commands.UpdateUser
{
    public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.UserUid)
                .NotEmpty().WithMessage("ID пользователя обязателен");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Имя обязательно для заполнения")
                .MaximumLength(50).WithMessage("Имя не должно превышать 50 символов");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Фамилия обязательна для заполнения")
                .MaximumLength(50).WithMessage("Фамилия не должна превышать 50 символов");

            RuleFor(x => x.MiddleName)
                .MaximumLength(50).WithMessage("Отчество не должно превышать 50 символов");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?[0-9\s\-\(\)]+$").WithMessage("Некорректный формат телефонного номера")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
        }
    }
} 