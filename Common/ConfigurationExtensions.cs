using Microsoft.Extensions.Configuration;

namespace Common;

public static class ConfigurationExtensions
{
    public static T GetRequired<T>(this IConfiguration configuration, string sectionName) where T : class
    {
        var section = configuration.GetRequiredSection(sectionName);
        var value = section.Get<T>();
        if (value != null)
            return value;
        throw new InvalidOperationException($"Section '{sectionName}' doesn't satisfy {typeof(T)}");
    }
}