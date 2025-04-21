using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Viridisca.Modules.Identity.Infrastructure.Persistence;

namespace Viridisca.Api.Extensions;

internal static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        // Применяем миграции для IdentityDbContext
        ApplyMigration<IdentityDbContext>(scope);

        // Здесь можно добавить другие вызовы для других DbContext'ов
        // ApplyMigration<ДругойDbContext>(scope);
    }

    private static void ApplyMigration<TDbContext>(IServiceScope scope) where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        context.Database.Migrate();
    }
} 