using Viridisca.Common.Domain;

namespace Viridisca.Modules.Academic.Domain.Groups
{
    public static class GroupErrors
    {
        public static readonly Error EmptyGroupCode = new(
            "Group.EmptyGroupCode",
            "Код группы не может быть пустым",
            ErrorType.Validation);
        
        public static readonly Error EmptyGroupName = new(
            "Group.EmptyGroupName",
            "Название группы не может быть пустым",
            ErrorType.Validation);
        
        public static readonly Error InvalidMaxStudents = new(
            "Group.InvalidMaxStudents",
            "Максимальное количество студентов должно быть положительным числом",
            ErrorType.Validation);
        
        public static readonly Error GroupNotFound = new(
            "Group.NotFound",
            "Группа не найдена",
            ErrorType.NotFound);
        
        public static readonly Error GroupCodeAlreadyExists = new(
            "Group.CodeAlreadyExists",
            "Группа с таким кодом уже существует",
            ErrorType.Conflict);
        
        public static readonly Error MaxStudentsReached = new(
            "Group.MaxStudentsReached",
            "Достигнуто максимальное количество студентов в группе",
            ErrorType.Conflict);
    }
} 