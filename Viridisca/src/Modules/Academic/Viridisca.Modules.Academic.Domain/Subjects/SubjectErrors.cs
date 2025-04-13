using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Subjects
{
    public static class SubjectErrors
    {
        public static readonly Error EmptySubjectCode = new(
            "Subject.EmptySubjectCode",
            "Код предмета не может быть пустым",
            ErrorType.Validation);
            
        public static readonly Error EmptySubjectName = new(
            "Subject.EmptySubjectName",
            "Название предмета не может быть пустым",
            ErrorType.Validation);
            
        public static readonly Error InvalidCredits = new(
            "Subject.InvalidCredits",
            "Количество кредитов должно быть положительным числом",
            ErrorType.Validation);
            
        public static readonly Error InvalidLessonsPerWeek = new(
            "Subject.InvalidLessonsPerWeek",
            "Количество уроков в неделю должно быть положительным числом",
            ErrorType.Validation);
            
        public static readonly Error SubjectNotFound = new(
            "Subject.NotFound",
            "Предмет не найден",
            ErrorType.NotFound);
            
        public static readonly Error SubjectCodeAlreadyExists = new(
            "Subject.CodeAlreadyExists",
            "Предмет с таким кодом уже существует",
            ErrorType.Conflict);
    }
} 