using Microsoft.Extensions.DependencyInjection;

namespace Neo.Extensions.Notifications
{
    public static class NeoNotification
    {
        public static IServiceCollection AddNotificationContext(this IServiceCollection services)
        {
            services.AddSingleton<INotificationContext, NotificationContext>();

            return services;
        }
    }
}
