using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Viridisca.Common.Application.Data;
using Viridisca.Modules.Identity.Domain.Repositories;

namespace Viridisca.Modules.Identity.Infrastructure;

public static class IdentityModule
{
    public static IServiceCollection AddIdentityModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDomainEventHandlers();

        services.AddIntegrationEventHandlers();

        services.AddInfrastructure(configuration);

        // services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddScoped<IPermissionService, PermissionService>();

        //services.Configure<KeyCloakOptions>(configuration.GetSection("Users:KeyCloak"));

        //services.AddTransient<KeyCloakAuthDelegatingHandler>();

        //services
        //    .AddHttpClient<KeyCloakClient>((serviceProvider, httpClient) =>
        //    {
        //        KeyCloakOptions keycloakOptions = serviceProvider
        //            .GetRequiredService<IOptions<KeyCloakOptions>>().Value;

        //        httpClient.BaseAddress = new Uri(keycloakOptions.AdminUrl);
        //    })
        //    .AddHttpMessageHandler<KeyCloakAuthDelegatingHandler>();

        //services.AddTransient<IIdentityProviderService, IdentityProviderService>();

        //services.AddDbContext<UsersDbContext>((sp, options) =>
        //    options
        //        .UseNpgsql(
        //            configuration.GetConnectionString("Database"),
        //            npgsqlOptions => npgsqlOptions
        //                .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Users))
        //        .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>())
        //        .UseSnakeCaseNamingConvention());

        //services.AddScoped<IUserRepository, UserRepository>();

        //services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UsersDbContext>());

        //services.Configure<OutboxOptions>(configuration.GetSection("Users:Outbox"));

        //services.ConfigureOptions<ConfigureProcessOutboxJob>();

        //services.Configure<InboxOptions>(configuration.GetSection("Users:Inbox"));

        //services.ConfigureOptions<ConfigureProcessInboxJob>();
    }

    private static void AddDomainEventHandlers(this IServiceCollection services)
    {
        //Type[] domainEventHandlers = Application.AssemblyReference.Assembly
        //    .GetTypes()
        //    .Where(t => t.IsAssignableTo(typeof(IDomainEventHandler)))
        //    .ToArray();

        //foreach (Type domainEventHandler in domainEventHandlers)
        //{
        //    services.TryAddScoped(domainEventHandler);

        //    Type domainEvent = domainEventHandler
        //        .GetInterfaces()
        //        .Single(i => i.IsGenericType)
        //        .GetGenericArguments()
        //        .Single();

        //    Type closedIdempotentHandler = typeof(IdempotentDomainEventHandler<>).MakeGenericType(domainEvent);

        //    services.Decorate(domainEventHandler, closedIdempotentHandler);
        //}
    }

    private static void AddIntegrationEventHandlers(this IServiceCollection services)
    {
        //Type[] integrationEventHandlers = Presentation.AssemblyReference.Assembly
        //    .GetTypes()
        //    .Where(t => t.IsAssignableTo(typeof(IIntegrationEventHandler)))
        //    .ToArray();

        //foreach (Type integrationEventHandler in integrationEventHandlers)
        //{
        //    services.TryAddScoped(integrationEventHandler);

        //    Type integrationEvent = integrationEventHandler
        //        .GetInterfaces()
        //        .Single(i => i.IsGenericType)
        //        .GetGenericArguments()
        //        .Single();

        //    Type closedIdempotentHandler =
        //        typeof(IdempotentIntegrationEventHandler<>).MakeGenericType(integrationEvent);

        //    services.Decorate(integrationEventHandler, closedIdempotentHandler);
        //}
    }
}
