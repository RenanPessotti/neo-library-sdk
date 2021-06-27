using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Neo.Extensions.Kafka.Abstractions
{
    public interface IEventBusPublisher
    {
        [Obsolete]
        Task PublishAsync<TEvent>(TEvent queue, string topicName) where TEvent : IEvent;
        Task PublishAsync<TEvent>(TEvent @event, Topics.EnumTopicsNeoPonto topic, string prefixEnv) where TEvent : IEvent;
    }
}
