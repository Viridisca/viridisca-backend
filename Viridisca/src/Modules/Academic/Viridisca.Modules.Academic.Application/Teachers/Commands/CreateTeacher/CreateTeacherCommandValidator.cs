using FluentValidation;

namespace Viridisca.Modules.Academic.Application.Teachers.Commands.CreateTeacher
{
    public sealed class CreateTeacherCommandValidator : AbstractValidator<CreateTeacherCommand>
    {
        public CreateTeacherCommandValidator()
        {
            RuleFor(x => x.UserUid)
                .NotEmpty().WithMessage("ID пользователя обязателен");

            RuleFor(x => x.EmployeeCode)
                .NotEmpty().WithMessage("Код сотрудника обязателен")
                .MaximumLength(20).WithMessage("Код сотрудника не должен превышать 20 символов")
                .Matches("^[A-Za-z0-9-]+$").WithMessage("Код сотрудника может содержать только буквы, цифры и знак -");

            RuleFor(x => x.HireDate)
                .NotEmpty().WithMessage("Дата приема на работу обязательна")
                .Must(date => date.Date <= System.DateTime.UtcNow.Date)
                .WithMessage("Дата приема на работу не может быть в будущем");

            RuleFor(x => x.Specialization)
                .NotEmpty().WithMessage("Специализация обязательна")
                .MaximumLength(100).WithMessage("Специализация не должна превышать 100 символов");

            RuleFor(x => x.Qualifications)
                .MaximumLength(2000).WithMessage("Квалификация не должна превышать 2000 символов");

            RuleFor(x => x.YearsOfExperience)
                .GreaterThanOrEqualTo(0).WithMessage("Опыт работы не может быть отрицательным");
        }
    }
} 