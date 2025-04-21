using System;
using FluentValidation;

namespace Viridisca.Modules.Identity.Application.Authentication.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email не может быть пустым")
            .EmailAddress().WithMessage("Email имеет неверный формат")
            .MaximumLength(256).WithMessage("Email не может быть длиннее 256 символов");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Имя пользователя не может быть пустым")
            .MinimumLength(3).WithMessage("Имя пользователя должно содержать не менее 3 символов")
            .MaximumLength(100).WithMessage("Имя пользователя не может быть длиннее 100 символов")
            .Matches("^[a-zA-Z0-9_]*$").WithMessage("Имя пользователя может содержать только латинские буквы, цифры и символ подчеркивания");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Пароль не может быть пустым")
            .MinimumLength(8).WithMessage("Пароль должен содержать не менее 8 символов")
            .Matches("[A-Z]").WithMessage("Пароль должен содержать как минимум одну заглавную букву")
            .Matches("[a-z]").WithMessage("Пароль должен содержать как минимум одну строчную букву")
            .Matches("[0-9]").WithMessage("Пароль должен содержать как минимум одну цифру")
            .Matches("[^a-zA-Z0-9]").WithMessage("Пароль должен содержать как минимум один специальный символ");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Имя не может быть пустым")
            .MaximumLength(100).WithMessage("Имя не может быть длиннее 100 символов");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Фамилия не может быть пустой")
            .MaximumLength(100).WithMessage("Фамилия не может быть длиннее 100 символов");

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20).WithMessage("Номер телефона не может быть длиннее 20 символов")
            .Matches(@"^(\+[0-9]{1,3})?[0-9()-]{6,14}$").When(x => !string.IsNullOrEmpty(x.PhoneNumber))
            .WithMessage("Номер телефона имеет неверный формат");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Дата рождения не может быть пустой")
            .LessThan(DateTime.Now).WithMessage("Дата рождения не может быть в будущем")
            .GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Возраст не может превышать 100 лет");
    }
} 