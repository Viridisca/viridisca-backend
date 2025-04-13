using FluentValidation;

namespace Viridisca.Modules.Academic.Application.Students.Commands.UpdateStudent
{
    public sealed class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
    {
        public UpdateStudentCommandValidator()
        {
            RuleFor(x => x.StudentUid)
                .NotEmpty().WithMessage("ID студента обязателен");

            RuleFor(x => x.EmergencyContactName)
                .MaximumLength(100).WithMessage("Имя контактного лица не должно превышать 100 символов");

            RuleFor(x => x.EmergencyContactPhone)
                .Matches(@"^\+?[0-9\s\-\(\)]+$").WithMessage("Некорректный формат телефонного номера")
                .When(x => !string.IsNullOrEmpty(x.EmergencyContactPhone));

            RuleFor(x => x.MedicalInformation)
                .MaximumLength(2000).WithMessage("Медицинская информация не должна превышать 2000 символов");
        }
    }
} 