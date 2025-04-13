// using Viridisca.Modules.Lessons.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Viridisca.Api.Extensions;

internal static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

       // ApplyMigration<LessonsDbContext>(scope);
    }

    private static void ApplyMigration<TDbContext>(IServiceScope scope) where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        context.Database.Migrate();
    }
}
