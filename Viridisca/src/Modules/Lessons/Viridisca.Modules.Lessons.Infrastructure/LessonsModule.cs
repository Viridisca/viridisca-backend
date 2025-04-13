using Viridisca.Common.Application.Data;
using Viridisca.Common.Infrastructure.Outbox;
using Viridisca.Common.Presentation.Endpoints;
using Viridisca.Modules.Lessons.Domain.Lessons; 
using Viridisca.Modules.Lessons.Infrastructure.Database;
using Viridisca.Modules.Lessons.Infrastructure.Lessons; 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Viridisca.Modules.Lessons.Infrastructure;

public static class LessonsModule
{
    public static IServiceCollection AddLessonsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        services.AddInfrastructure(configuration);

        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database")!;

        services.AddDbContext<LessonsDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    databaseConnectionString,
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Lessons))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>()));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<LessonsDbContext>());

        services.AddScoped<ILessonRepository, LessonRepository>(); 
    }
}