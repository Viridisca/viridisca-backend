using System;
using FluentValidation;

namespace Viridisca.Modules.Identity.Application.Users.Commands.CreateUser
{
    public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email обязателен для заполнения")
                .EmailAddress().WithMessage("Некорректный формат email")
                .MaximumLength(100).WithMessage("Email не должен превышать 100 символов");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Имя пользователя обязательно для заполнения")
                .MinimumLength(3).WithMessage("Имя пользователя должно содержать не менее 3 символов")
                .MaximumLength(50).WithMessage("Имя пользователя не должно превышать 50 символов")
                .Matches("^[a-zA-Z0-9._-]+$").WithMessage("Имя пользователя может содержать только буквы, цифры и символы ._-");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Пароль обязателен для заполнения")
                .MinimumLength(8).WithMessage("Пароль должен содержать не менее 8 символов")
                .Matches("[A-Z]").WithMessage("Пароль должен содержать хотя бы одну заглавную букву")
                .Matches("[a-z]").WithMessage("Пароль должен содержать хотя бы одну строчную букву")
                .Matches("[0-9]").WithMessage("Пароль должен содержать хотя бы одну цифру")
                .Matches("[^a-zA-Z0-9]").WithMessage("Пароль должен содержать хотя бы один специальный символ");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Имя обязательно для заполнения")
                .MaximumLength(50).WithMessage("Имя не должно превышать 50 символов");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Фамилия обязательна для заполнения")
                .MaximumLength(50).WithMessage("Фамилия не должна превышать 50 символов");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?[0-9\s\-\(\)]+$").WithMessage("Некорректный формат телефонного номера")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Дата рождения обязательна для заполнения")
                .LessThan(DateTime.UtcNow.AddYears(-5)).WithMessage("Пользователь должен быть старше 5 лет")
                .GreaterThan(DateTime.UtcNow.AddYears(-100)).WithMessage("Дата рождения указана некорректно");
        }
    }
} 