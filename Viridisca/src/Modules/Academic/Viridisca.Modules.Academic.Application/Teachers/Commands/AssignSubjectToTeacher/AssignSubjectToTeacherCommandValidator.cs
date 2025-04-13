using FluentValidation;

namespace Viridisca.Modules.Academic.Application.Teachers.Commands.AssignSubjectToTeacher
{
    public sealed class AssignSubjectToTeacherCommandValidator : AbstractValidator<AssignSubjectToTeacherCommand>
    {
        public AssignSubjectToTeacherCommandValidator()
        {
            RuleFor(x => x.TeacherUid)
                .NotEmpty().WithMessage("ID преподавателя обязателен");

            RuleFor(x => x.SubjectUid)
                .NotEmpty().WithMessage("ID предмета обязателен");
        }
    }
} 