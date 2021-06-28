using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neo.Extensions.Kafka.Abstractions
{
    public interface IEventBusSubscriber
    {
        [Obsolete]
        Task SubscribeEventAsync(string queue, CancellationToken cancellationToken);
        Task SubscribeEventAsync(Topics.EnumTopics topic, string prefixEnv, CancellationToken cancellationToken);
    }
}
