using System.ComponentModel;

namespace Viridisca.Modules.Identity.Domain.Models
{
    public enum RoleType
    {
        [Description("Системный администратор")]
        SystemAdmin = 1,

        [Description("Директор учебного заведения")]
        SchoolDirector,

        [Description("Заведующий учебной частью")]
        AcademicAffairsHead,

        [Description("Преподаватель")]
        Teacher,

        [Description("Куратор группы")]
        GroupCurator,

        [Description("Студент")]
        Student,

        [Description("Родитель")]
        Parent,

        [Description("Методист")]
        EducationMethodist,

        [Description("Финансовый менеджер")]
        FinancialManager,

        [Description("Менеджер по качеству")]
        QualityAssuranceManager,

        [Description("Контент-менеджер")]
        ContentManager,

        [Description("Аналитик")]
        DataAnalyst,
        
        [Description("Библиотекарь")]
        Librarian,
        
        [Description("Психолог")]
        Psychologist,
        
        [Description("Медицинский работник")]
        HealthcareSpecialist,
        
        [Description("Технический специалист")]
        TechnicalSupport,

        [Description("Гость")]
        Guest
    }
} 