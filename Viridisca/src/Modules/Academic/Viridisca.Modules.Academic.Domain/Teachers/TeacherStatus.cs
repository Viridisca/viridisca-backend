using System.ComponentModel;

namespace Viridisca.Modules.Academic.Domain.Teachers
{
    /// <summary>
    /// Статус преподавателя
    /// </summary>
    public enum TeacherStatus
    {
        [Description("Активный")]
        Active = 1,
        
        [Description("В отпуске")]
        OnLeave = 2,
        
        [Description("Уволен")]
        Terminated = 3,
        
        [Description("Временно неактивен")]
        Inactive = 4
    }
} 