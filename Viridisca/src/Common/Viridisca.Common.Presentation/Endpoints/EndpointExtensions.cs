using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System.Reflection;

namespace Viridisca.Common.Presentation.Endpoints;
 
/// <summary>
/// Extension methods for endpoint registration
/// </summary>
public static class EndpointExtensions
{
    /// <summary>
    /// Adds endpoints from the specified assembly to the service collection
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="assemblies">The assemblies containing endpoint definitions</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddEndpoints(this IServiceCollection services, params Assembly[] assemblies)
    {
        ServiceDescriptor[] serviceDescriptors = [.. assemblies
            .SelectMany(a => a.GetTypes())
            .Where(type => typeof(IEndpoint)
                .IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))];

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    /// <summary>
    /// Maps all registered endpoints to the endpoint route builder
    /// </summary>
    /// <param name="app">The application builder</param>
    /// <returns>The application builder for chaining</returns>
    public static WebApplication MapEndpoints(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
    {
        IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }

        return app;
    }
} 
