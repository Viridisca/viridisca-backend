using System.ComponentModel;

namespace Viridisca.Modules.Academic.Domain.Students
{
    /// <summary>
    /// Тип родственной связи родителя со студентом
    /// </summary>
    public enum ParentRelationType
    {
        [Description("Мать")]
        Mother = 1,
        
        [Description("Отец")]
        Father = 2,
        
        [Description("Опекун")]
        Guardian = 3,
        
        [Description("Бабушка")]
        Grandmother = 4,
        
        [Description("Дедушка")]
        Grandfather = 5,
        
        [Description("Другое")]
        Other = 6
    }
} 