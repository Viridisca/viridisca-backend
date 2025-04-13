using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Students
{
    public static class StudentErrors
    {
        public static readonly Error EmptyStudentCode = new(
            "Student.EmptyStudentCode",
            "Код студента не может быть пустым",
            ErrorType.Validation);
        
        public static readonly Error StudentNotFound = new(
            "Student.NotFound",
            "Студент не найден",
            ErrorType.NotFound);
        
        public static readonly Error AlreadyInGroup = new(
            "Student.AlreadyInGroup",
            "Студент уже находится в этой группе",
            ErrorType.Conflict);
        
        public static readonly Error NotInGroup = new(
            "Student.NotInGroup",
            "Студент не находится в этой группе",
            ErrorType.Conflict);
        
        public static readonly Error AlreadyHasParent = new(
            "Student.AlreadyHasParent",
            "Этот пользователь уже является родителем этого студента",
            ErrorType.Conflict);
    }
} 