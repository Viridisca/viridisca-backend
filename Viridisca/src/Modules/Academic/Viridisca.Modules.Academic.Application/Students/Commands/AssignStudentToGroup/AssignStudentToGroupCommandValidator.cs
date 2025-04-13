using FluentValidation;

namespace Viridisca.Modules.Academic.Application.Students.Commands.AssignStudentToGroup
{
    public sealed class AssignStudentToGroupCommandValidator : AbstractValidator<AssignStudentToGroupCommand>
    {
        public AssignStudentToGroupCommandValidator()
        {
            RuleFor(x => x.StudentUid)
                .NotEmpty().WithMessage("ID студента обязателен");

            RuleFor(x => x.GroupUid)
                .NotEmpty().WithMessage("ID группы обязателен");
        }
    }
} 