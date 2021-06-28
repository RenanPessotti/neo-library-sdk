using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Neo.Extensions.Redis
{
    public static class NeoRedis
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, RedisSettings redisSettings)
        {
            var redisConfig = new ConfigurationOptions
            {
                EndPoints =
                {
                    { redisSettings.Hostname, redisSettings.Port }
                },
                Ssl = true,
                Password = redisSettings.Password,
                ConnectRetry = redisSettings.ConnectRetry,
                ConnectTimeout = redisSettings.ConnectTimeout,
                DefaultDatabase = redisSettings.DefaultDatabase,
                ClientName = redisSettings.ClientName,
                ReconnectRetryPolicy = new LinearRetry(redisSettings.LinearRetry)
            };

            services.AddSingleton((IConnectionMultiplexer)ConnectionMultiplexer.Connect(redisConfig));

            services.AddSingleton(container => container.GetService<ConnectionMultiplexer>().GetDatabase());

            services.AddSingleton<IRedisService, RedisService>();

            return services;
        }
    }
}
