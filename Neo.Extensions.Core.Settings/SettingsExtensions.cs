using Microsoft.Extensions.Configuration;

namespace Neo.Extensions.Core.Settings
{
    public static class SettingsExtensions
    {
        public static T GetSettingsFor<T>(this IConfiguration configuration)
        {
            return configuration.GetSection(typeof(T).Name).Get<T>();

        }
    }
}
