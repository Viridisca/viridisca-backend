using FluentValidation;

namespace Viridisca.Modules.Academic.Application.Students.Commands.CreateStudent
{
    public sealed class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentCommandValidator()
        {
            RuleFor(x => x.UserUid)
                .NotEmpty().WithMessage("ID пользователя обязателен");

            RuleFor(x => x.StudentCode)
                .NotEmpty().WithMessage("Код студента обязателен")
                .MaximumLength(20).WithMessage("Код студента не должен превышать 20 символов")
                .Matches("^[A-Za-z0-9-]+$").WithMessage("Код студента может содержать только буквы, цифры и знак -");

            RuleFor(x => x.EnrollmentDate)
                .NotEmpty().WithMessage("Дата зачисления обязательна")
                .Must(date => date.Date <= System.DateTime.UtcNow.Date)
                .WithMessage("Дата зачисления не может быть в будущем");
        }
    }
}