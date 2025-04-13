using FluentValidation;

namespace Viridisca.Modules.Academic.Application.Students.Commands.AddStudentParent
{
    public sealed class AddStudentParentCommandValidator : AbstractValidator<AddStudentParentCommand>
    {
        public AddStudentParentCommandValidator()
        {
            RuleFor(x => x.StudentUid)
                .NotEmpty().WithMessage("ID студента обязателен");

            RuleFor(x => x.ParentUserUid)
                .NotEmpty().WithMessage("ID пользователя родителя обязателен");

            RuleFor(x => x.Relation)
                .IsInEnum().WithMessage("Указана некорректная степень родства");
        }
    }
} 