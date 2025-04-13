using System.ComponentModel;

namespace Viridisca.Modules.Academic.Domain.Students
{
    /// <summary>
    /// Статус студента в системе
    /// </summary>
    public enum StudentStatus
    {
        [Description("Активный")]
        Active = 1,
        
        [Description("Академический отпуск")]
        AcademicLeave = 2,
        
        [Description("Отчислен")]
        Expelled = 3,
        
        [Description("Выпущен")]
        Graduated = 4,
        
        [Description("Переведен")]
        Transferred = 5,
        
        [Description("Приостановлен")]
        Suspended = 6
    }
} 