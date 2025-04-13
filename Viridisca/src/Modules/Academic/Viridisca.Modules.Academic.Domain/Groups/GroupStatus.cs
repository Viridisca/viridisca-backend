using System.ComponentModel;

namespace Viridisca.Modules.Academic.Domain.Groups
{
    /// <summary>
    /// Статус учебной группы
    /// </summary>
    public enum GroupStatus
    {
        [Description("Активная")]
        Active = 1,
        
        [Description("Формирование")]
        Forming = 2,
        
        [Description("Приостановлена")]
        Suspended = 3,
        
        [Description("Завершена")]
        Completed = 4,
        
        [Description("Архивная")]
        Archived = 5
    }
} 