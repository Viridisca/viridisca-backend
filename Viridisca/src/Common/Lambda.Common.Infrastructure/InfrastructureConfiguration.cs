using Viridisca.Common.Application.Clock;
using Viridisca.Common.Application.Data;
using Viridisca.Common.Infrastructure.Clock;
using Viridisca.Common.Infrastructure.Data;
using Viridisca.Common.Infrastructure.Outbox;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;

namespace Viridisca.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string databaseConnectionString)
    {
        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);

        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>(); 
        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.TryAddSingleton<PublishDomainEventsInterceptor>();

        return services;
    }

}
