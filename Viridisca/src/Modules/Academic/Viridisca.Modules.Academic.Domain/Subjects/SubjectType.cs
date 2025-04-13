using System.ComponentModel;

namespace Viridisca.Modules.Academic.Domain.Subjects
{
    /// <summary>
    /// Тип учебного предмета
    /// </summary>
    public enum SubjectType
    {
        [Description("Обязательный")]
        Required = 1,
        
        [Description("Факультативный")]
        Elective = 2,
        
        [Description("Специализированный")]
        Specialized = 3,
        
        [Description("Практикум")]
        Practicum = 4,
        
        [Description("Семинар")]
        Seminar = 5
    }
} 