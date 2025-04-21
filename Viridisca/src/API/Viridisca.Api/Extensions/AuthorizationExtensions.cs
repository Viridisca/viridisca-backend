using Viridisca.Modules.Identity.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Viridisca.Api.Extensions;

/// <summary>
/// Extension methods for authorization configuration
/// </summary>
public static class AuthorizationExtensions
{
    /// <summary>
    /// Adds authorization policies for the application
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddAuthorizationWithPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            // Add default policies for roles
            foreach (RoleType role in Enum.GetValues<RoleType>())
            {
                string roleName = role.ToString();
                options.AddPolicy(roleName, policy => policy.RequireRole(roleName));
            }

            // Admin policy - requires SystemAdmin role
            options.AddPolicy("RequireAdminRole", policy => 
                policy.RequireRole(RoleType.SystemAdmin.ToString()));

            // School management policy - for school administrators
            options.AddPolicy("SchoolManagement", policy => 
                policy.RequireRole(
                    RoleType.SystemAdmin.ToString(),
                    RoleType.SchoolDirector.ToString(),
                    RoleType.AcademicAffairsHead.ToString()));

            // Teaching staff policy
            options.AddPolicy("TeachingStaff", policy => 
                policy.RequireRole(
                    RoleType.Teacher.ToString(),
                    RoleType.GroupCurator.ToString(),
                    RoleType.EducationMethodist.ToString()));

            // Student management policy
            options.AddPolicy("StudentManagement", policy => 
                policy.RequireRole(
                    RoleType.Teacher.ToString(), 
                    RoleType.GroupCurator.ToString(),
                    RoleType.AcademicAffairsHead.ToString()));

            // Content management policy
            options.AddPolicy("ContentManagement", policy => 
                policy.RequireRole(
                    RoleType.ContentManager.ToString(),
                    RoleType.EducationMethodist.ToString()));

            // Finance management policy
            options.AddPolicy("FinanceManagement", policy => 
                policy.RequireRole(
                    RoleType.SystemAdmin.ToString(),
                    RoleType.SchoolDirector.ToString(),
                    RoleType.FinancialManager.ToString()));

            // Data analysis policy
            options.AddPolicy("DataAnalysis", policy => 
                policy.RequireRole(
                    RoleType.DataAnalyst.ToString(),
                    RoleType.QualityAssuranceManager.ToString(),
                    RoleType.SchoolDirector.ToString()));

            // Support staff policy
            options.AddPolicy("SupportStaff", policy => 
                policy.RequireRole(
                    RoleType.Librarian.ToString(),
                    RoleType.Psychologist.ToString(),
                    RoleType.HealthcareSpecialist.ToString(),
                    RoleType.TechnicalSupport.ToString()));
        });

        return services;
    }
} 