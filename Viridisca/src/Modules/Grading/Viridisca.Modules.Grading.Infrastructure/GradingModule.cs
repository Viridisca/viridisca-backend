using Viridisca.Common.Application.Data;
using Viridisca.Common.Infrastructure.Outbox;
using Viridisca.Common.Presentation.Endpoints;
using Viridisca.Modules.Grading.Application.Services;
using Viridisca.Modules.Grading.Domain.Repositories;
using Viridisca.Modules.Grading.Infrastructure.EF;
using Viridisca.Modules.Grading.Infrastructure.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;  
using Viridisca.Common.Application.Identity;
using Viridisca.Common.Infrastructure.Identity;
using Viridisca.Modules.Grading.Presentation;

namespace Viridisca.Modules.Grading.Infrastructure;

public static class GradingModule
{
    public static IServiceCollection AddGradingModule(
        this IServiceCollection services,
        IConfiguration configuration)
    { 
        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddEndpoints(AssemblyReference.Assembly);

        services.AddApplication();
        services.AddInfrastructure(configuration);

        return services;
    }

    private static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<GradeRevisionsService>();

        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database")!;

        services.AddDbContext<GradingDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    databaseConnectionString,
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Grading))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>()));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<GradingDbContext>());

        // Регистрация репозиториев
        services.AddScoped<IGradeRevisionsRepository, GradeRevisionsRepository>();
        services.AddScoped<IGradeRepository, GradeRepository>();
    }
} 