using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Teachers
{
    public static class TeacherErrors
    {
        public static readonly Error EmptyEmployeeCode = new(
            "Teacher.EmptyEmployeeCode",
            "Код сотрудника не может быть пустым",
            ErrorType.Validation);
            
        public static readonly Error InvalidHourlyRate = new(
            "Teacher.InvalidHourlyRate",
            "Ставка оплаты не может быть отрицательной",
            ErrorType.Validation);
            
        public static readonly Error TeacherNotFound = new(
            "Teacher.NotFound",
            "Преподаватель не найден",
            ErrorType.NotFound);
            
        public static readonly Error TeacherCodeAlreadyExists = new(
            "Teacher.CodeAlreadyExists",
            "Преподаватель с таким кодом уже существует",
            ErrorType.Conflict);
            
        public static readonly Error AlreadyTeachesSubject = new(
            "Teacher.AlreadyTeachesSubject",
            "Преподаватель уже ведет этот предмет",
            ErrorType.Conflict);
    }
} 