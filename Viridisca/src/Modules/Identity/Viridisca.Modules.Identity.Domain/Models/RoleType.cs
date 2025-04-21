using System.ComponentModel;

namespace Viridisca.Modules.Identity.Domain.Models;

/// <summary>
/// Represents the types of roles available in the system
/// </summary>
public enum RoleType
{
    /// <summary>
    /// System administrator with full access
    /// </summary>
    [Description("System Administrator")]
    SystemAdmin = 1,

    /// <summary>
    /// Director of educational institution
    /// </summary>
    [Description("School Director")]
    SchoolDirector = 2,

    /// <summary>
    /// Head of academic department
    /// </summary>
    [Description("Academic Affairs Head")]
    AcademicAffairsHead = 3,

    /// <summary>
    /// Faculty member giving lessons
    /// </summary>
    [Description("Teacher")]
    Teacher = 4,

    /// <summary>
    /// Curator responsible for student groups
    /// </summary>
    [Description("Group Curator")]
    GroupCurator = 5,

    /// <summary>
    /// Learner enrolled in courses
    /// </summary>
    [Description("Student")]
    Student = 6,

    /// <summary>
    /// Parent or guardian of a student
    /// </summary>
    [Description("Parent")]
    Parent = 7,

    /// <summary>
    /// Curriculum methodology specialist
    /// </summary>
    [Description("Education Methodist")]
    EducationMethodist = 8,

    /// <summary>
    /// Financial operations manager
    /// </summary>
    [Description("Financial Manager")]
    FinancialManager = 9,

    /// <summary>
    /// Education quality specialist
    /// </summary>
    [Description("Quality Assurance Manager")]
    QualityAssuranceManager = 10,

    /// <summary>
    /// Learning content specialist
    /// </summary>
    [Description("Content Manager")]
    ContentManager = 11,

    /// <summary>
    /// Analytics specialist
    /// </summary>
    [Description("Data Analyst")]
    DataAnalyst = 12,

    /// <summary>
    /// Library resources manager
    /// </summary>
    [Description("Librarian")]
    Librarian = 13,

    /// <summary>
    /// Student psychological support
    /// </summary>
    [Description("Psychologist")]
    Psychologist = 14,

    /// <summary>
    /// Health services specialist
    /// </summary>
    [Description("Healthcare Specialist")]
    HealthcareSpecialist = 15,

    /// <summary>
    /// IT support specialist
    /// </summary>
    [Description("Technical Support")]
    TechnicalSupport = 16,

    /// <summary>
    /// Unauthenticated or limited access user
    /// </summary>
    [Description("Guest")]
    Guest = 17
}

/// <summary>
/// Extension methods for RoleType enum
/// </summary>
public static class RoleTypeExtensions
{
    /// <summary>
    /// Gets the display name for a role type from the Description attribute
    /// </summary>
    /// <param name="roleType">The role type</param>
    /// <returns>The display name</returns>
    public static string GetRoleDisplayName(this RoleType roleType)
    {
        var fieldInfo = roleType.GetType().GetField(roleType.ToString());
        
        if (fieldInfo == null)
        {
            return roleType.ToString();
        }
        
        var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
        
        return attributes.Length > 0 ? attributes[0].Description : roleType.ToString();
    }
}
