using System.Collections.Generic;

namespace Neo.Extensions.Notifications
{
    public interface INotificationContext
    {
        IReadOnlyCollection<Notification> Notifications { get; }
        bool HasNotifications { get; }
        void AddNotification(string key, string message);
    }
}
