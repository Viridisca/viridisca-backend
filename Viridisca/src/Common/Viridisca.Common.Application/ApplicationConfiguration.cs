using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;

namespace Viridisca.Common.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        Assembly[] moduleAssemblies)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(moduleAssemblies);

            // AddBehavior
            // config.AddOpenBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>)); 
        }); 
         
        services.AddValidatorsFromAssemblies(moduleAssemblies, includeInternalTypes: true);

        return services;
    }
}
