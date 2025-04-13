namespace Viridisca.Api.Extensions;

public static class ConfigurationExtensions
{
    public static string GetConnectionStringOrThrow(this IConfiguration configuration, string name)
    {
        return configuration.GetConnectionString(name) ??
               throw new InvalidOperationException($"The connection string {name} was not found");
    }

    public static T GetValueOrThrow<T>(this IConfiguration configuration, string name)
    {
        return configuration.GetValue<T?>(name) ??
               throw new InvalidOperationException($"The connection string {name} was not found");
    }

    internal static void AddModuleConfiguration(this IConfigurationBuilder configurationBuilder, string[] modules)
    {
        foreach (string module in modules)
        {
            configurationBuilder.AddJsonFile($"modules.{module}.json", false, true);
            configurationBuilder.AddJsonFile($"modules.{module}.Development.json", true, true);
        }
    }
}
